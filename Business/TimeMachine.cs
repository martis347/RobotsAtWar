using System.Threading;
using Business.Enums;

namespace Business
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