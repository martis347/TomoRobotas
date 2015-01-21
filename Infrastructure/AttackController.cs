using System;
using System.Collections.Generic;
using System.Web.Http;
using Business;
using Communication;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class AttackController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new[] { "_value1, _value2" };           
        }

        // GET api/<controller>/5
        public int Get(int war1)
        {
            return war1;
        }

        // POST api/<controller>
        public int Post([FromBody] string strength)
        {
            return BattleField.Warrior1.GetAttacked(Int32.Parse(strength));
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