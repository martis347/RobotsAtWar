﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business;

namespace WebAPI
{
    public class StartController : ApiController
    {

        private Battlefield battlefield;
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public void  Get(string strategy)
        {
            battlefield.Fight();
            Console.WriteLine(strategy);
        }

        //public void Get()
        //{
        //    Console.WriteLine("adasdasdasd");
        //}

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