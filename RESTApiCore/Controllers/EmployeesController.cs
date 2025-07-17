using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore; // For ExecuteSqlRaw
using RESTApiCore.Data;
using RESTApiCore.Models;
using RESTApiCore.Models.Entities;

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

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var param = new[]
            {
                 new SqlParameter("@Name", addEmployeeDto.Name),
                 new SqlParameter("@Email", addEmployeeDto.Email),
                 new SqlParameter("@Phone", addEmployeeDto.Phone),
                 new SqlParameter("@Salary", addEmployeeDto.Salary),
            };

            dbContext.Database.ExecuteSqlRaw("EXEC InsertEmployee @Name, @Email, @Phone, @Salary", param);

            var employeeEntity = new Employee() {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };

            //dbContext.Employees.Add(employeeEntity);
            //dbContext.SaveChanges();
            return StatusCode(201, new { message = "Employee Created Successfully", employeeEntity });
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound("Employee not found for given employee id. Might be terminated");
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployee)
        {
            var employee = dbContext.Employees.Find(id);

            if(employee is null)
            {
                return NotFound();
            }

            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.Phone = updateEmployee.Phone;
            employee.Salary= (decimal)updateEmployee.Salary;

            dbContext.SaveChanges();
            return Ok(updateEmployee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if(employee is null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok("Successfully terminated Employee");
        }


    }
}
