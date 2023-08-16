using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.States;

public class LobbyState : IPayloadedState<UiLobbyView>
{
    private GameStateMachine _stateMachine;
    private UiLobbyView _lobbyView;
    private LoadingCurtain _curtain;

    public LobbyState(GameStateMachine stateMachine, LoadingCurtain curtain)
    {
        _stateMachine = stateMachine;
        _curtain = curtain;
    }

    private void PlayButtonClickEventHandler()
    {
        _stateMachine.Enter<LoadLevelState, float>(AssetLevels.LargeBorderSize);
    }

    public void Enter(UiLobbyView lobbyView)
    {
        _lobbyView = lobbyView;
        _lobbyView.OnPlayButtonClicked += PlayButtonClickEventHandler;
        _curtain.Hide();
    }

    public void Exit()
    {
        _lobbyView.OnPlayButtonClicked -= PlayButtonClickEventHandler;
    }
}
