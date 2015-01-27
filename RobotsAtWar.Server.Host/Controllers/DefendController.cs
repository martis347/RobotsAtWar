using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class DefendController : ApiController
    {
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            int time = Int32.Parse(Regex.Match(value, @"\d+").Value);
            var name = Regex.Replace(value, @"[\d-]", string.Empty);
           // string name = value.Remove(0, 1);

            BattleFieldSingleton.BattleField.GetWarriorByName(name).Defend(time);

        }

    }
}