using HRMS_BackEnd.Database.DTOs.LeaveDTOs;
using HRMS_BackEnd.Database.Models;
using HRMS_BackEnd.Repositries.LeaveRepositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRMS_BackEnd.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LeaveController : ControllerBase
    {

        private readonly ILeaveRepository leaveRepository;
        private readonly ILogger<LeaveController> logger;
        public LeaveController(ILeaveRepository leaveRepository, ILogger<LeaveController> logs)
        {
            this.leaveRepository = leaveRepository;
            logger = logs;
        }

        //For List of Emplyoee History
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("GetLeaveRequestEmployee/{id}")]
        public async Task<IActionResult> GetLeaveRequestEmployee(int id)
        {

            logger.LogInformation("GetLeaveRequestEmployee API has been invoked!");

            if (id.GetType() != typeof(int))
            {
                throw new Exception();
            }

            var dataDTO = new List<LeaveRequestDTOs>();


            logger.LogInformation("LeaveRepository is executed");
            var data = await leaveRepository.GetLeaveRequestEmployee(id);

            if (data == null)
            {
                logger.LogInformation("No data found!!");
                return NotFound();
            }
            else
            {

                foreach (var item in data)
                {

                    dataDTO.Add(new LeaveRequestDTOs
                    {
                        LeaveRequestId = item.LeaveRequestId,
                        EmployeeId = item.EmployeeId,
                        LeaveType = item.LeaveType,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Status = item.Status,
                        Reason = item.Reason

                    });

                }
                logger.LogInformation("Data is returned successfully");
             
                return Ok(dataDTO);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("AddLeaveRequest")]
        public async Task<IActionResult> AddLeaveRequest([FromBody]AddLeaveDTOs dataToBeAdded)
        {
            
            try
            {

                if (ModelState.IsValid)
                {
                    var dataModel = new LeaveRequest()
                    {
                        EmployeeId = dataToBeAdded.EmployeeId,
                        LeaveType = dataToBeAdded.LeaveType,
                        StartDate = dataToBeAdded.StartDate,
                        EndDate = dataToBeAdded.EndDate,
                        Status = "Pending",
                        Reason = dataToBeAdded.Reason,

                    };
                    string data = await leaveRepository.AddLeaveRequest(dataModel);

                    return Ok(data);
                }
                else
                    return BadRequest(ModelState);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [MapToApiVersion("1.0")]
        [HttpDelete("RemoveLeaveRequest")]
        public async Task<IActionResult> RemoveLeaveRequest([FromBody]RemoveLeaveDTO dataToBeRemoved)
        {
            try
            {
                var dataModel = new LeaveRequest
                {
                    EmployeeId = dataToBeRemoved.EmployeeId,
                    StartDate = dataToBeRemoved.StartDate
                };

               

                var checkRequest = await leaveRepository.RemoveLeaveRequest(dataModel);

                if (checkRequest == "Committed")
                {
                    return Ok("Leave Request has been withdrawn");
                }
                else if (checkRequest == "Current")
                {
                    return Conflict("Cannot be withdrawn, Already has been past its date");
                }
                else
                    return NotFound("Does not exist");

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [MapToApiVersion("1.0")]
        [HttpPut("RemoveRangedLeaveRequest")]
        public async Task<IActionResult> DetectLeaveRequest([FromBody]DetuctLeaveRequestDTO dataRequest)
        {
            try
            {
                var dataModel = new LeaveRequest
                {

                    EmployeeId = dataRequest.EmployeeId,
                    LeaveType = dataRequest.LeaveType,
                    StartDate = dataRequest.StartDate,
                    EndDate = dataRequest.EndDate,
                };


                await leaveRepository.DetuctLeaveRequestRep(dataModel, dataRequest.CancelledFromDate, dataRequest.CancelledToDate);

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
