using System.Collections.Generic;
using System.Web.Http;
using Business;
using Business.Enums;

namespace RobotsAtWar.Controllers
{
    public class CheckController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get(int id)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public int Get()
        {
           
           return Battlefield.Warrior1.CheckMe();
            
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}