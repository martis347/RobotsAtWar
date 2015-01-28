using System;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class RegistrationController : ApiController
    {
        private static int _number = 0;
        public void Get()
        {
            Console.WriteLine("Connected!");
        }
        // POST api/<controller>
        public string Post([FromBody]string warriorName)
        {
            _number++;
            BattleFieldSingleton.BattleField.RegisterWarrior(warriorName);

            Console.WriteLine("New warrior named: " + warriorName + " registered");
            while (_number != 2)
            {

            }
            _number = 0;
            return "You have been connected";
        }



    }
}