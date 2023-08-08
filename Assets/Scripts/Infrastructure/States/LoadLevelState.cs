using Pewpew.Infrastructure.Factory;
using UnityEngine;

namespace Pewpew.Infrastructure.States
{
    internal class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            GameObject player = _gameFactory.CreatePlayer(at: GameObject.FindWithTag(InitialPointTag));
            CameraFollow(player);

            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject gameObject)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);
        }

        public void Exit()
        {
            _curtain.Hide();
        }
    }
}