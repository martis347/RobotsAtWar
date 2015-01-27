using System.Linq;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class DefenceController : ApiController
    {
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            int time = value.ElementAt(0) - 48;
            string name = value.Remove(0, 1);

            BattleFieldSingleton.BattleField.GetWarriorByName(name).Defend(time);

        }

    }
}