﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Business;

namespace WebAPI
{
    public class AttackControllers : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get(int id)
        {
            return new [] { "value1, value2 "};
        }

       
        public void Get()
        {
            
        }


        public void Post([FromBody]string strength)
        {
            Battlefield._warrior.GetAttacked(Int32.Parse(strength));           
        }
       
       
        public void Put(int id, [FromBody]string value)
        {
            
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}