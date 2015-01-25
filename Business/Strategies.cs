using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Enums;
using Action = Business.Enums.Action;

namespace Business
{
    public class Strategies
    {
        public static readonly List<Command> Aggressive = new List<Command>
        {
           // new Command(Action.Attack,Strength.Weak),
           //// new Command(Action.Check),
           // //new Command(Action.Attack),
           // new Command(Action.Attack,Strength.Weak),
           //// new Command(Action.Check),
           // new Command(Action.Attack,Strength.Weak),
           // //new Command(Action.Check)
           new Command(Action.Check),
           new Command(Action.Attack,Strength.Weak)
            
        };

        public  readonly List<Command> StupidAggressive = new List<Command>
        {
            new Command(Action.Attack, Strength.Strong),
            new Command(Action.Check),
            new Command(Action.Attack, Strength.Strong),
            new Command(Action.Check),
            new Command(Action.Attack, Strength.Strong),
            new Command(Action.Check),

        };

        public readonly List<Command> Defensive = new List<Command>
        {
            new Command(Action.Defend, 2),
            new Command(Action.Check),
            new Command(Action.Defend, 1),
            new Command(Action.Check),
            new Command(Action.Rest, 1),
            new Command(Action.Check),
        };

        public readonly List<Command> Mixed = new List<Command>
        {
            new Command(Action.Defend, 2),
            new Command(Action.Check),
            new Command(Action.Rest, 1),
            new Command(Action.Check),
            new Command(Action.Attack, Strength.Weak),
            new Command(Action.Check),
        };
        public readonly List<Command> Default = new List<Command>
        {
            new Command(Action.Defend, 3),
        };
        
        

    }
}
