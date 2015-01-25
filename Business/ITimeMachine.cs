namespace Business
{
    public interface ITimeMachine
    {
        void Sleep(int miliseconds, WarriorState state, IResetable resetable);

    }
}