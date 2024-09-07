using HRMS_BackEnd.Database.DTOs.RoleDTOs;
using HRMS_BackEnd.Database.Models;
using HRMS_BackEnd.Repositories.RolePosition_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_BackEnd.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RolePositionController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;
        private readonly ILogger<RolePositionController> logger;

        public RolePositionController(IRoleRepository roleRepository, ILogger<RolePositionController> logger)
        {
            this.roleRepository = roleRepository;
            this.logger = logger;
        }
        [HttpPut("RoleChangeRequest")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> RoleRequestChange([FromBody]RolesRequestDTO requestDTO)
        {
            try
            {
                string result = await roleRepository.RoleRequestChange(requestDTO.EmployeeID, requestDTO.RoleName, requestDTO.Reason);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
