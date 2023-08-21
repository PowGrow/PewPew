using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Factory;
using Pewpew.Logic.Map;
using Pewpew.Player;
using System;
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

        private float _borderScaleConjuctor;
        private int _asteroidDensity;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IBulletFactory bulletFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _bulletFactory = bulletFactory;
        }

        public void Enter(LoadLevelPayload payload)
        {
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
            Generator mapGenerator = InstantiateAsteroidMap(_gameFactory, border.transform.localScale.x, _asteroidDensity);

            _stateMachine.Enter<GameLoopState>();
        }

        private Generator InstantiateAsteroidMap(IGameFactory gameFactory, float borderScale, int asteroidDensity)
        {
            Generator mapGenerator;
            if (asteroidDensity != 0)
            {
                mapGenerator = new Generator(gameFactory, asteroidDensity);
                mapGenerator.GenerateAsteroids(Convert.ToInt32(borderScale * AssetLevels.BorderSizeСoefficient));
                return mapGenerator;
            }
            return null;
        }

        private GameObject InstantiatePlayer(IGameFactory gameFactory)
        {
            var player = gameFactory.CreatePlayer(at: GameObject.FindWithTag(InitialPointTag));
            var playerInfo = player.GetComponent<Stats>();
            CreateBulletPool(playerInfo.Weapon, playerInfo.Damage);
            player.GetComponent<Guns>().Initialize();
            player.GetComponent<Health>().Initialize();
            CameraFollow(player);
            return player;
        }

        private void CreateBulletPool(WeaponType weaponType, float Damage)
        {
            if (AssetPath.WeaponAmmoPrefabPaths[weaponType] != null)
                _bulletFactory.CreateBulletPool(AssetPath.WeaponAmmoPrefabPaths[weaponType], Damage);
        }

        private void CameraFollow(GameObject gameObject)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);
        }
    }
}