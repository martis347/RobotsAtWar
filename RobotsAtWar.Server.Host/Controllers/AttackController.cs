﻿using System.Linq;
using System.Web.Http;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class AttackController : ApiController
    {
        // POST api/<controller>
        public Response Post([FromBody]string value)
        {
            int power = value.ElementAt(0) - 48;
            string name = value.Remove(0, 1);

            var str = Strength.None;
            switch (power)
            {
                case 4:
                    str = Strength.Strong;
                    break;
                case 2:
                    str = Strength.Normal;
                    break;
                case 1:
                    str = Strength.Weak;
                    break;
            }
            return BattleFieldSingleton.BattleField.GetWarriorByName(name).Attack(name, str);
        }

    }
}