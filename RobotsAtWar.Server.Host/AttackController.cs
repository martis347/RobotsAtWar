using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Enums;

namespace RobotsAtWar.Server.Host
{
    public class AttackController : ApiController
    {
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            int power = Int32.Parse(value);
            var str = Strength.None;
            switch (power)
            {
                case 4:
                    str=Strength.Strong;
                    break;
                case 2:
                    str = Strength.Normal;
                    break;
                case 1:
                    str = Strength.Weak;
                    break;
            }
            Warrior.Attack(str);
        }

    }
}