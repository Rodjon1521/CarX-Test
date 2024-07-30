using StaticData;

namespace Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
    }
}