using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeCreateDTO employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _employeeService.Add(employee);
                if (result > 0)
                {

                    return Ok(result);

                }
                else
                {
                    return BadRequest("Enter valid data");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public IActionResult GetEmployee(int id)
        {
            try
            {
                return Ok(_employeeService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _employeeService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeDetailsDTO employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _employeeService.Update(employee);

                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Enter valid data");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public IActionResult ListEmployees(int start = 0, int size = 8)
        {

            try
            {
                return Ok(_employeeService.List(start, size));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpPost]
        public IActionResult CheckUniqueEmail(string email,int ? id)
        {
            try
            {
                return Ok(_employeeService.CheckUniqueEmail(email,id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public IActionResult GetEmployees()
        {
            try
            {
                return Ok(_employeeService.GetEmployees());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
