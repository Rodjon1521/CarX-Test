using System;
using System.Collections.Generic;
using Enemy;
using Infrastructure.Factory;
using Infrastructure.Services;
using ObjectsPool;
using Tower;
using UnityEngine;

[RequireComponent(typeof(ObjectPoolComponent))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float delayToSpawnInSec = 1f;
    
    [SerializeField] private Transform pathParent;
    private IGameFactory _factory;
    
    private float passedTime = 0f;
    
    private EnemyDeath _enemyDeath;
    private EnemyMovement _enemyMovement;
    
    private ObjectPoolComponent _pool;
        
    private void Awake()
    {
        _pool = GetComponent<ObjectPoolComponent>();
        _factory = AllServices.Container.Single<IGameFactory>();
        CreatePool();
    }

    private void Update()
    {
        if (passedTime <= delayToSpawnInSec)
        {
            passedTime += Time.deltaTime;
        }
        else
        {
            _pool.TakeObject();
            passedTime -= delayToSpawnInSec;
        }
    }

    private PooledObject Spawn()
    {
        var enemy = _factory.CreateEnemy(pathParent, transform);
        enemy.transform.localPosition = transform.position;
        EnemyFinderHelper.enemies.Add(enemy.GetComponent<Enemy.Enemy>());

        PooledObject pooledObject = enemy.GetComponent<PooledObject>();
        _enemyDeath = enemy.GetComponent<EnemyDeath>();
        return pooledObject;
    }
    
    public void CreatePool()
    {
        var pool = new Queue<PooledObject>(_pool.count);
        for (int i = 0; i < _pool.count; i++)
        {
            var enemy = Spawn();
            enemy.gameObject.SetActive(false);
            enemy.Pool = _pool;
            pool.Enqueue(enemy);
        }
        _pool.SetupPool(pool);
    }
}