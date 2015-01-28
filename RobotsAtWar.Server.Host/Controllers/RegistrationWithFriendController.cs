using System;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class RegistrationWithFriendController : ApiController
    {
        private static int _numberWithFriends = 0;

        // POST api/<controller>
        public string Post([FromBody]string names)
        {
            string[] words = names.Split(',');

            _numberWithFriends++;
            BattleFieldSingleton.BattleField.RegisterWarriorWithFriend(words[0],words[1]);

            Console.WriteLine("New warrior named: " + words[0] + " registered");
            while (_numberWithFriends != 2)
            {

            }
            _numberWithFriends = 0;
            return "You have been connected";
        }



    }
}