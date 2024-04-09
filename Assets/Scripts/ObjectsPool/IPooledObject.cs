namespace TowerDefence
{
    public interface IPooledObject
    {
        void ObjectReuse();
        bool IsActive { get; }
    }
}