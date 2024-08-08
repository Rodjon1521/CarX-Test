namespace PoolingSystem
{
    public interface IPoolObject<T>
    {
        T @group { get; }
        void Create();
        void OnPush();
        void OnFailedPush();
    }
}