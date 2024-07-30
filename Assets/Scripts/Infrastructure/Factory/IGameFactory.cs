using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateEnemy(Transform pathParent, Transform parent);
    }
}