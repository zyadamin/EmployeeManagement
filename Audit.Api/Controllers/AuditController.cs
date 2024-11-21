using Audit.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Audit.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpPost]
        public IActionResult GetEmployeeAudit(int employeeId)
        {
            try
            {
                return Ok(_auditService.GetAllByEmployeeId(employeeId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
