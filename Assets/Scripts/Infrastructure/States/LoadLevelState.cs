using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Factory;
using Pewpew.Infrastructure.Services.Inventory;
using Pewpew.Logic.Inventory;
using Pewpew.Logic.Loot;
using Pewpew.Logic.Map;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Pewpew.Infrastructure.States
{
    internal class LoadLevelState : IPayloadedState<LoadLevelPayload>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string BorderInitialPointTag = "BorderInitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IBulletFactory _bulletFactory;
        private readonly IItemsInfoService _itemsInfoService;

        private LootTable _lootTable;
        private int _asteroidDensity;
        private float _borderScaleConjuctor;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IBulletFactory bulletFactory, IItemsInfoService itemsInfoService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _bulletFactory = bulletFactory;
            _itemsInfoService = itemsInfoService;
        }

        public void Enter(LoadLevelPayload payload)
        {
            _lootTable = payload.LootTable;
            _asteroidDensity = payload.AsteroidDensity;
            _borderScaleConjuctor = payload.BorderSize;
            _curtain.Show();
            _sceneLoader.Load(AssetLevels.GameLevelName, OnLoaded);

        }

        public void Exit()
        {
            _curtain.Hide();
        }
        private void OnLoaded()
        {
            GameObject player = InstantiatePlayer(_gameFactory);
            GameObject border = _gameFactory.CreateGameBorder(_borderScaleConjuctor, at: GameObject.FindWithTag(BorderInitialPointTag));
            List<Asteroid> asteroids = GenerateAsteroidMap(_gameFactory, border.transform.localScale.x, _asteroidDensity);
            LootDistributor distributor = InstantiateLootDistributor(_gameFactory, _lootTable,_itemsInfoService.Items,asteroids);

            _stateMachine.Enter<GameLoopState>();
        }



        private LootDistributor InstantiateLootDistributor(IGameFactory gameFactory, LootTable lootTable, Items items, List<Asteroid> asteroids)
        {
            return new LootDistributor(gameFactory, lootTable, items, asteroids);
        }

        private List<Asteroid> GenerateAsteroidMap(IGameFactory gameFactory, float borderScale, int asteroidDensity)
        {
            Generator mapGenerator;
            if (asteroidDensity != 0)
            {
                mapGenerator = new Generator(gameFactory, asteroidDensity);
                return mapGenerator.GenerateAsteroids(Convert.ToInt32(borderScale * AssetLevels.BorderSizeСoefficient));
            }
            return null;
        }

        private GameObject InstantiatePlayer(IGameFactory gameFactory)
        {
            var player = gameFactory.CreatePlayer(at: GameObject.FindWithTag(InitialPointTag));
            CameraFollow(player);
            return player;
        }

        private void CameraFollow(GameObject gameObject)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);
        }
    }
}