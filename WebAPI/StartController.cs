using System;
using System.Collections.Generic;
using System.Web.Http;
using Business;

namespace WebAPI
{
    public class StartController : ApiController
    {
        private Battlefield _batt = new Battlefield();
        


        private string value1, value2;
        // GET api/<controller>
        public IEnumerable<string> Get(int id)
        {
            
            return new [] { value1, value2 };

        }

        // GET api/<controller>/5
        //public void Get([FromBody]string id)
        //{
        //    //batt.Fight("asad");
        //    Console.WriteLine(id);
            
        //}
        public void Get()
        {
            _batt.Fight("Aggressive");
            Console.WriteLine( _batt.GiveWarr1().Check().Life);
        }


        public string Post(string id)
        {
            _batt.Fight("Aggresive");
            Console.WriteLine(_batt.GiveWarr1().Check().Life);
            Console.WriteLine(_batt.GiveWarr2().Check().Life);

            return id;
        }
        //public string Post()
        //{
        //    //batt.Fight(val);
        //    Console.WriteLine("dsfsdf");
        //    return "sffgsf";
        //}
       
        public void Put(int id, [FromBody]string value)
        {
            Console.WriteLine(id+value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}