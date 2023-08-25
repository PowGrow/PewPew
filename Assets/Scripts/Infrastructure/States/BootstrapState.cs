using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Factory;
using Pewpew.Infrastructure.Services;
using Pewpew.Infrastructure.Services.Inventory;
using Pewpew.Logic.Inventory;
using Pewpew.Services.Inputs;
using PewPew.Infrastructure.AssetManagment;
using System.Collections.Generic;
using UnityEngine;

namespace Pewpew.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private const float BulletPoolSize = 100;
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private AllServices _services;


        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            Application.targetFrameRate = 60;
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }
        public void Exit()
        {

        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLobbyState, string>("Lobby");

        private void RegisterServices()
        {
            //TEST ITEMS
            var itemsInfo = new List<ItemInfo>
            {
                new ItemInfo(1, "Copper Ore", "Just copper ore", "Copper Ore", Resources.Load<GameObject>(AssetItems.ItemsPrefabPath + "Copper Ore"), true, 100),
                new ItemInfo(2, "Iron Ore", "Just iron ore", "Iron Ore", Resources.Load<GameObject>(AssetItems.ItemsPrefabPath + "Iron Ore"), true, 100)
            };
            var items = new Items(itemsInfo);
            //

            _services.RegisterSingle<IInputService>(new StandaloneInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<IBulletFactory>(new BulletFactory(_services.Single<IAssetProvider>(), BulletPoolSize));
            _services.RegisterSingle<IItemsInfoService>(new ItemsInfoService(items));
        }
    }
}