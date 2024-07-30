using System;
using System.Linq;
using DG.Tweening.Plugins.Core.PathCore;
using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;

namespace TowerDefence
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float delayToSpawnInSec = 1f;
        [SerializeField, Range(1, 20)] private int enemiesCount = 10;
        private IGameFactory _factory;
        [SerializeField] private Transform pathParent;
        private float passedTime = 0f;
        
        
        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        private void Update()
        {
            if (passedTime <= delayToSpawnInSec)
            {
                passedTime += Time.deltaTime;
            }
            else
            {
                Spawn();
                passedTime -= delayToSpawnInSec;
            }
        }

        public void Spawn()
        {
            var enemy = _factory.CreateEnemy(pathParent, transform);
        }
    }
}