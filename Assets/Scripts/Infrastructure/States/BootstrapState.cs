using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Factory;
using Pewpew.Infrastructure.Services;
using Pewpew.Infrastructure.Services.Inventory;
using Pewpew.Services.Inputs;

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
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }
        public void Exit()
        {

        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLobbyState, string>("Lobby");

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(new StandaloneInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<IBulletFactory>(new BulletFactory(_services.Single<IAssetProvider>(), BulletPoolSize));
            _services.RegisterSingle<IItemsInfoService>(new ItemsInfoService());
        }
    }
}