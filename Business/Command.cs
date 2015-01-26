using Business.Enums;

namespace Business
{
    public class Command
    {
        public Action Action;
        public int Time;
        public Strength Power;
        //For def and rest only
        public Command(Action action, int time)
        {
            switch (action)
            {
                case Action.Attack:
                    return;
                case Action.Check:
                    return;
            }
            this.Action = action;
            this.Time = time;
        }

        //For attack only
        public Command(Action action, Strength str)
        {
            if (action != Action.Attack)
            {
                return;
            }
            this.Action = action;
            this.Power = str;
        }
        //For check only
        public Command(Action action)
        {
            if (action != Action.Check)
            {
                return;
            }
            this.Action = action;
        }
    }
}