using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Mvc;
using SouthStar.VehSch.Core.Setting.Services;

namespace SouthStar.VehSch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private DriverService _driverService;
        private readonly IServiceProvider _serviceProvider;

        public ValuesController(DriverService driverService,IServiceProvider serviceProvider)
        {
            _driverService = driverService;
            _serviceProvider = serviceProvider;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>>Get()
        {

            return new string[] { "value1", "value2" };
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
