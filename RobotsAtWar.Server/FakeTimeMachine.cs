namespace RobotsAtWar.Server
{
    public class FakeTimeMachine : ITimeMachine
    {
        public void Sleep(int miliseconds, WarriorState state, IResetable resetable)
        {
        }

        public void Reset(IResetable resetable)
        {
            
        }
    }
}