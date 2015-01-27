using System;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class RestController : ApiController
    {
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            int time = Int32.Parse(Regex.Match(value, @"\d+").Value);
            var name = Regex.Replace(value, @"[\d-]", string.Empty);

            BattleFieldSingleton.BattleField.GetWarriorByName(name).Rest(time);
        }

    }
}