using System;
using System.Web.Http;

namespace RobotsAtWar.Server.Host
{
    public class RegistrationController : ApiController
    {
        public void Get()
        {
            Console.WriteLine("Connected!");
        }
        // POST api/<controller>
        public string Post([FromBody]string warriorName)
        {
            BattleFieldSingleton.BattleField.RegisterWarrior(warriorName);
            
            Console.WriteLine("New warrior named: "+warriorName+" registered");
            return "You have been connected";
        }

        
        
    }
}