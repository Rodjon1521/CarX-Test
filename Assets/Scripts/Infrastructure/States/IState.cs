namespace Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    
    public interface IPayLoadedState<in TPayLoad> : IState
    {
        void Enter(TPayLoad payLoad);
    }
    
    public interface IExitableState
    {
        void Exit();
    }
}