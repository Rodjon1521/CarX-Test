using Infrastructure.Factory;
using Infrastructure.Services;
using PoolingSystem;
using StaticData;
using Tower;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float delayToSpawnInSec = 1f;
        [SerializeField] private Transform pathParent;
        
        private IGameFactory _factory;
        private float _passedTime = 0f;
        
        private void Awake()
        {
           
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        private void Update()
        {
            if (_passedTime <= delayToSpawnInSec)
            {
                _passedTime += Time.deltaTime;
            }
            else
            {
                var enemy = _factory.CreateEnemy(pathParent).GetComponent<Enemy>();
                var obj = PoolManager.instance.PopOrCreate<Enemy>(enemy, transform);
                EnemyFinderHelper.enemies.Add(obj);
                _passedTime -= delayToSpawnInSec;
            }
        }
    }
}