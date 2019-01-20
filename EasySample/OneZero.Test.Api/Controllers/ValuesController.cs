using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using OneZero.EntityFramwork.Log;
using OneZero.Domain.Audits;
using OneZero.Entity;
using OneZero.EntityFramwork.DatabaseContext.EFContext;
using OneZero.Model.Identity;

namespace OneZero.Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ValuesController : ControllerBase
    {
        private ILogger<ValuesController> _logger;
        private LoggerFactory _loggerFactory;
        private DbContext _dbContext;
        public class Audit
        {
            public string id;

        }

        public ValuesController(ILogger<ValuesController> logger, MSSqlContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public string test = "";
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
           //var efLogger =_loggerFactory.CreateLogger("Microsoft.EntityFrameworkCore.Database.Command");
           // efLogger.LogInformation("TEST1");
            _logger.LogInformation("Order {orderid} created for {user}", 42, "Kenny");
            _logger.LogError("错误测试");
            _logger.LogCritical("崩溃测试");
            _dbContext.Set<User>().FirstOrDefault();
            _dbContext.Set<User>().AddAsync(new User());
            throw new ArgumentOutOfRangeException();
            //DefaultDbActionAudit audit = new DefaultDbActionAudit(_dbContext);
            //var t = (DbRequestAudit)audit.RecordQuery();
            //    _logger.LogInformation("数据库：{DbName},Connextion:{connect},时间：{DateTime},事务：{trans},语句：{sql}", _dbContext.Database.ProviderName, _dbContext.Database.GetDbConnection().ConnectionString, _dbContext.Database.GetDbConnection().DataSource, _dbContext.Database.GetDbConnection().Database, t.CommandSql);

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
