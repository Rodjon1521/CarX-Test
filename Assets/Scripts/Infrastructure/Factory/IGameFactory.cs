using Infrastructure.Services;
using Tower;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateEnemy(Transform pathParent, Transform parent);
    }
}