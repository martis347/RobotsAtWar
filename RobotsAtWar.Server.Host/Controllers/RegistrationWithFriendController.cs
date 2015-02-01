using System;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class RegistrationWithFriendController : ApiController
    {
        private static int number = 0;
        public void Get()
        {
            Console.WriteLine("Connected!");
        }
        // POST api/<controller>
        public string Post([FromBody]string warriorName)
        {
            string[] names = warriorName.Split(',');
            number++;
            BattleFieldSingleton.BattleField.RegisterWarriorWithFriend(names[0],names[1]);

            Console.WriteLine("New warrior named: " + names[0] + " registered");
            while (number != 2)
            {

            }
            return "You have been connected";
        }



    }
}