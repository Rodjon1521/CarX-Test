using Infrastructure.Factory;
using TowerDefence;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string EnemySpawnerTag = "EnemySpawner";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
            
        }

        private void OnLoaded()
        {
            InitGameWorld();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
        }


    }
}