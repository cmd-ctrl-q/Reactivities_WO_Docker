using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace DatingApp.API.Controllers
{
    // route to access this controller
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // conventional using _ with private variables
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;
        }

        // GET api/values (route of this controller)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> Get()
        {
            // await return of whats coming back from db
            // passing off request to call db from different thread then await that task to be completed but were not blockign the theads the requests come in on
            var values = await _context.Values.ToListAsync();
            return Ok(values); // not recommended (not asynchronous)
            // recommended approach is to make queries to db bc they have potential to be long running queries so they should be asynchronous
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> Get(int id)
        {
            // singledefault: return element of a sequence that matches a condition
            // findasync: which will find entity with given primary key values
            var value = await _context.Values.FindAsync(id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
