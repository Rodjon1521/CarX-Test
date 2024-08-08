using Enemy;
using Infrastructure.Services;
using StaticData;
using Tower;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateEnemy(Transform pathParent);
    }
}