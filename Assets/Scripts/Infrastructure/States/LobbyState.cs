using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.States;
using Pewpew.Logic.Loot;
using System.Collections.Generic;

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
        var testLootTable = new LootTable();
        testLootTable.Add(AsteroidTypes.Copper, 1, 100);
        testLootTable.Add(AsteroidTypes.Iron, 2, 100);
        var mineralChances = new Dictionary<AsteroidTypes, float>()
        {
            {AsteroidTypes.Empty, 80f },
            {AsteroidTypes.Copper, 10f },
            {AsteroidTypes.Iron, 10f },
        };
        _stateMachine.Enter<LoadLevelState, LoadLevelPayload>(new LoadLevelPayload(AssetLevels.MediumBorderSize,5, testLootTable, mineralChances));
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
