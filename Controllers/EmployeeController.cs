
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationsApi.Data;
using NotificationsApi.Dtos;
using NotificationsApi.Models;



namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
            => await _context.Employees.OrderBy(e => e.Id).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }

            return Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDto empDto)
        {
            // ✅ Check if email already exists (to prevent duplicates)
            var existing = await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == empDto.Email);
            if (existing != null)
            {
                return Conflict(new { message = "Employee with this email already exists" });
            }

           
            var employee = new Employee
            {
                Name = empDto.Name,
                Email = empDto.Email,
                Department = empDto.Department,
                Salary = empDto.Salary, 
                Created_At = DateTime.UtcNow
            };
         
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Employee Added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error Adding employee", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto empDto)
        {
            
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }

            
            employee.Name = empDto.Name;
            employee.Email = empDto.Email;
            employee.Department = empDto.Department;
            employee.Salary = empDto.Salary;
            employee.Created_At = DateTime.UtcNow; 

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Employee updated successfully" }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating employee", details = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
          
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }

            
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

          
            return Ok(new { message = "Employee deleted successfully" });
        }

    }
}
