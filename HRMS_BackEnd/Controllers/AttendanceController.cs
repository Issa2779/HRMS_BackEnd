using HRMS_BackEnd.Database.DTOs.AttendaceDTOs;
using HRMS_BackEnd.Database.Models;
using HRMS_BackEnd.Repositories.Attendace_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HRMS_BackEnd.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AttendanceController : ControllerBase
    {

        private readonly IAttendanceRepository attendanceRepository;
        private readonly ILogger<AttendanceController> logger;
        

        public AttendanceController(IAttendanceRepository attendanceRepository, ILogger<AttendanceController> logger)
        {
            this.attendanceRepository = attendanceRepository;
            this.logger = logger;
        }


        [HttpPost]
        [Route("CheckInAttendance")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> CheckInAttendanceRequest([FromBody]AttendanceCheckInOutRequestDTO requestDTO)
        {
            try
            {

                DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
                TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
                
                var data = new AttendanceRecord
                {
                    EmployeeId = requestDTO.EmployeeId,
                    AttendanceDate = DateOnly.FromDateTime(DateTime.Now),
                    Status = "Pending",
                    CheckInTime = currentTime,
                };
                await attendanceRepository.CheckInAttendanceRequest(data);

                return Ok();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("CheckOutAttendance")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> checkOutAttendanceRequest([FromBody] AttendanceCheckInOutRequestDTO requestDTO)
        {
            try
            {
                TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

                var data = new AttendanceRecord
                {
                    EmployeeId = requestDTO.EmployeeId,
                    CheckOutTime = currentTime,
                };

                await attendanceRepository.CheckOutAttendanceRequest(data);
                return Ok();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
