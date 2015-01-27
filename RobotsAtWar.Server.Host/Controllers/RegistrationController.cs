using System;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class RegistrationController : ApiController
    {
        private static int number = 0;
        public void Get()
        {
            Console.WriteLine("Connected!");
        }
        // POST api/<controller>
        public string Post([FromBody]string warriorName)
        {
            number++;
            BattleFieldSingleton.BattleField.RegisterWarrior(warriorName);

            Console.WriteLine("New warrior named: " + warriorName + " registered");
            while (number != 2)
            {

            }
            return "You have been connected";
        }



    }
}