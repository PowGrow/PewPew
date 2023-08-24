using Pewpew.Infrastructure.Factory;
using Pewpew.Infrastructure;
using Pewpew.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class LoadLobbyState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;

    public LoadLobbyState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
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
        GameObject lobbyHudGameObject = _gameFactory.CreateLobbyHud();
        UiLobbyView uiLobbyView = lobbyHudGameObject.GetComponent<UiLobbyView>();

        _stateMachine.Enter<LobbyState,UiLobbyView>(uiLobbyView);
    }

    public void Exit()
    {
    }
}
