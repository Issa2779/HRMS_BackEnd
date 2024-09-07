using HRMS_BackEnd.Database.DTOs.PendingDTO;
using HRMS_BackEnd.Database.Models;
using HRMS_BackEnd.Repositories.PendingStatus_Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace HRMS_BackEnd.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PendingStatusController : ControllerBase
    {


        private readonly IPendingRepository pendingRepository;
        private readonly ILogger<PendingStatusController> logger;


        public PendingStatusController(IPendingRepository pendingRepository, ILogger<PendingStatusController> logger)
        {
            this.logger = logger;
            this.pendingRepository = pendingRepository;
        }

        [HttpGet]
        [Route("GetPendingLeavesRequest/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPendingLeavesRequests(int id)
        {
            try
            {
                var data = await pendingRepository.GetPendingLeaveRequests(id);

                return (data != null) ? Ok(data) : BadRequest("Data not Found");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPendingPositionRoleRequest/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPendingPositionRoleRequest(int id)
        {

            try
            {
                var data = await pendingRepository.GetPendingRoleChangeRequests(id);

                return (data != null) ? Ok(data) : BadRequest("Data not Found");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        [HttpPut("ConfirmPendingLeaveRequest")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> ConfirmPendingLeaveRequest([FromBody]PendingLeaveRequestDTO requestDTO)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (requestDTO != null)
                    {
                        int leaveRequestId = requestDTO.LeaveRequestId;
                        string? status = requestDTO.ConfirmationStatus.ToLower();

                        var data = await pendingRepository.ConfirmPendingLeaveRequest(leaveRequestId, status);

                        return Ok(data);
                    }
                    else
                    {
                        return BadRequest();
                    } 
                }
                else
                {
                    return BadRequest();
                }

            }
            catch(Exception  ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("ConfirmPendingRoleRequest")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> ConfirmPendingRoleRequest([FromBody]PendingRoleChangeRequest requestDTO)
        {
            if (requestDTO != null)
            {
                int roleRequestId = requestDTO.RoleRequestId;
                int employeeId = requestDTO.EmployeeId;
                string? status = requestDTO.ConfirmationStatus.ToLower();

                var data = await pendingRepository.ConfirmPendingRoleChangeRequest(roleRequestId, status, employeeId);

                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
