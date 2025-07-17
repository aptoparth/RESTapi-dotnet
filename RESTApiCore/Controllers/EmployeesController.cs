using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTApiCore.Data;

namespace RESTApiCore.Controllers
{
    // localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ApplicationDBcontext dbContext;

        public EmployeesController(ApplicationDBcontext dBContext)
        {
            this.dbContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allemployees = dbContext.Employees.ToList();
            return Ok(allemployees);
        }
    }
}
