using System.Threading;
using State = RobotsAtWar.Server.Enums.State;

namespace RobotsAtWar.Server
{
    public class TimeMachine : ITimeMachine
    {
        public void Sleep(int miliseconds, WarriorState state, IResetable resetable)
        {
            Thread.Sleep(miliseconds);
            if (state.State != State.Interrupted)
            {
                resetable.Reset();
            }
        }

        
    }

    public interface IResetable
    {
        void Reset();
    }
}