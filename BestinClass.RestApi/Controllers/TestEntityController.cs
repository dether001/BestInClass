using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BestinClass.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestEntityController : ControllerBase
    {

        private readonly ITestEntityService _testEntityService;

        public TestEntityController(ITestEntityService testEntityService)
        {
            _testEntityService = testEntityService;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TestEntity>> Get()
        {
            return _testEntityService.GetAllTestEntites();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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