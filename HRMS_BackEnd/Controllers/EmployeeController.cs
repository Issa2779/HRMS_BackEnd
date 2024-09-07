using HRMS_BackEnd.Database.DTOs.EmployeeDTOs;
using HRMS_BackEnd.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Versioning;
using HRMS_BackEnd.Database.Context;
using HRMS_BackEnd.Database.Models;
using Microsoft.AspNetCore.Authorization;

namespace HRMS_BackEnd.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRespository employeeRespository;
        private readonly ILogger<EmployeeController> logger;


        public EmployeeController(IEmployeeRespository employeeRep, ILogger<EmployeeController> logger)
        {
            employeeRespository = employeeRep;
            this.logger = logger;
        }
        [HttpGet]
        [Route("GetEmployee/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetEmployeeById(int id) {


            var data = await employeeRespository.GetEmployeeById(id);


            if (data == null) {

                return NotFound("User does not exist!!");

            }

            var dataDTO = new EmployeeDataResponse
            {

                EmployeeId = data.EmployeeId,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Phone = data.Phone,
                HireDate = data.HireDate,
                Position = data.Position,
                DepartmentId = data.DepartmentId,
                RoleId = data.RoleId,

            };

            return Ok(dataDTO);

        }

        //api/v{version:apiVersion}/[controller]?pageNumber=?&pageSize=?
        [HttpGet("GetAllEmployees")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllEmployees([FromQuery] int pageNumber, int pageSize)
        {

            logger.LogDebug("EXECUTION OF API: GetAllEmployees Data has been invoked!");
            logger.LogDebug($"EXECUTION OF API: pageNumber: {pageNumber} and pageSize {pageSize}");


            var data = await employeeRespository.GetAllEmployees(pageNumber, pageSize);
            var responseDTO = new List<EmployeeDataResponse>();

            if (data != null)
            {

                foreach (var item in data)
                {
                    responseDTO.Add(new EmployeeDataResponse
                    {
                        EmployeeId = item.EmployeeId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        Phone = item.Phone,
                        HireDate = item.HireDate,
                        Position = item.Position,
                        DepartmentId = item.DepartmentId,
                        RoleId = item.RoleId,
                    });
                }

                return Ok(responseDTO);
            }
            else
                return BadRequest("No Employees Data Found");





        }

        //[HttpPost("AddEmployee")]
        //[MapToApiVersion("1.0")]
        //public async Task<IActionResult> AddEmployee([FromBody]EmployeeDataRequest requestDTO)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        //To Context Data
        //        var data = new Employee
        //        {

        //            FirstName = requestDTO.FirstName,
        //            LastName = requestDTO.LastName,
        //            Email = requestDTO.Email,
        //            Phone = requestDTO.Phone,
        //            HireDate = requestDTO.HireDate,
        //            Position = requestDTO.Position,
        //            DepartmentId = requestDTO.DepartmentId,
        //            RoleId = requestDTO.RoleId,

        //        };

        //        await employeeRespository.AddEmployee(data);
        //        return Ok("Data has been inserted successfully");

        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}

        [HttpPut("UpdateEmployeeInfo")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateEmployeeInfo([FromBody] UpdateEmployeeRequest requestDTO)
        {

            var data = new Employee
            {
                EmployeeId = requestDTO.EmployeeID,
                Phone = requestDTO.Phone,
            };

            await employeeRespository.UpdateEmployee(data);

            return Ok();
        }


        

    }
}
