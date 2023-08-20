using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Factory;
using Pewpew.Logic.Map;
using Pewpew.Player;
using System;
using UnityEngine;

namespace Pewpew.Infrastructure.States
{
    internal class LoadLevelState : IPayloadedState<float>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string BorderInitialPointTag = "BorderInitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IBulletFactory _bulletFactory;

        private float _levelSize;
        private Generator _levelGenerator;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IBulletFactory bulletFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _bulletFactory = bulletFactory;
        }

        public void Enter(float levelSize)
        {
            _levelSize = levelSize;
            _curtain.Show();
            _sceneLoader.Load(AssetLevels.GameLevelName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }
        private void OnLoaded()
        {
            GameObject player = _gameFactory.CreatePlayer(at: GameObject.FindWithTag(InitialPointTag));
            var playerInfo = player.GetComponent<Stats>();
            CreateBulletPool(playerInfo.Weapon, playerInfo.Damage);
            player.GetComponent<Guns>().Initialize();
            player.GetComponent<Health>().Initialize();
            GameObject border = _gameFactory.CreateGameBorder(_levelSize, at: GameObject.FindWithTag(BorderInitialPointTag));
            CameraFollow(player);

            _levelGenerator = new Generator(_gameFactory,5);
            _levelGenerator.GenerateAsteroids(500);

            _stateMachine.Enter<GameLoopState>();
        }

        private void CreateBulletPool(WeaponType weaponType, float Damage)
        {
            switch (weaponType)
            {
                case WeaponType.MachineGun:
                    _bulletFactory.CreateBulletPool(AssetPath.BulletPrefabPath, Damage);
                    break;
                case WeaponType.RocketLauncher:
                    _bulletFactory.CreateBulletPool(AssetPath.RocketPrefabPath, Damage);
                    break;
                case WeaponType.Laser:
                    break;
                default:
                    break;
            }
        }

        private void CameraFollow(GameObject gameObject)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);
        }

        private void GeneratePerlinNoise()
        {

        }

    }
}