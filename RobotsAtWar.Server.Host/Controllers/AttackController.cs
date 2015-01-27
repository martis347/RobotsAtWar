using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class AttackController : ApiController
    {
        // POST api/<controller>
        public Response Post([FromBody]string value)
        {
            int power = Int32.Parse(Regex.Match(value, @"\d+").Value);
            var name = Regex.Replace(value, @"[\d-]", string.Empty);

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