using Pewpew.Infrastructure.Factory;
using Pewpew.Infrastructure.Services;
using Pewpew.Infrastructure.Services.Inventory;
using System;
using System.Collections.Generic;

namespace Pewpew.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services) 
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, services.Single<IGameFactory>(), services.Single<IBulletFactory>(), services.Single<IItemsInfoService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
                [typeof(LoadLobbyState)] = new LoadLobbyState(this, sceneLoader, curtain, services.Single<IGameFactory>()),
                [typeof(LobbyState)] = new LobbyState(this, curtain),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }
        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;

    }
}

