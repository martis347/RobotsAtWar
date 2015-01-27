using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class MyInfoController : ApiController
    {
        // POST api/<controller>
        public WarriorState Post([FromBody]string value)
        {
            return BattleFieldSingleton.BattleField.GetWarriorByName(value).MyInfo();
        }

    }
}