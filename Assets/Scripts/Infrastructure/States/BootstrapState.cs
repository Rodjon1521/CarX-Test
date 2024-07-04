using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using StaticData;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }
        
        public void Exit()
        {
            
        }
        
        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>("Game");
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssets>(new AssetProvider());
            RegisterStaticData();
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>(),
                _services.Single<IStaticDataService>()));
        }

        private void RegisterStaticData()
        {
            var staticData = new StaticDataService();
            staticData.LoadMonsters();
            _services.RegisterSingle<IStaticDataService>(staticData);
        }
    }
}