using Pewpew.Logic.Inventory;
using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.States;
using Pewpew.Logic.Loot;

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
        var testItems = new Items();
        var testLootTable = new LootTable(testItems);
        testLootTable.Add("Asteroid", 1, 20);
        testLootTable.Add("Asteroid", 2, 20);
        _stateMachine.Enter<LoadLevelState, LoadLevelPayload>(new LoadLevelPayload(AssetLevels.MediumBorderSize,5, testLootTable,testItems));
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
