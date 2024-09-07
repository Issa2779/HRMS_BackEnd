using HRMS_BackEnd.Database.Context;
using HRMS_BackEnd.Database.DTOs.AuthDTOs;
using HRMS_BackEnd.Database.Models;
using HRMS_BackEnd.Repositories.EmployeeRepository;
using HRMS_BackEnd.Repositories.TokenRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_BackEnd.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IEmployeeRespository employeeRespository;
        private readonly UserManager<IdentityUser> _userManager;    
        private readonly IJwtTokenRepo jwtTokenRepo;

        public AuthenticateController(UserManager<IdentityUser> userManager, IJwtTokenRepo jwtTokenRepo, IEmployeeRespository employeeRespository)
        {
            _userManager = userManager;
            this.jwtTokenRepo = jwtTokenRepo;
            this.employeeRespository = employeeRespository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO authDTO)
        {

            if (ModelState.IsValid)
            {
                //To Context Data
                var data = new Employee
                {
                    FirstName = authDTO.FirstName,
                    LastName = authDTO.LastName,
                    Email = authDTO.Email,
                    Phone = authDTO.Phone,
                    HireDate = authDTO.HireDate,
                    Position = authDTO.Position,
                    DepartmentId = authDTO.DepartmentId,
                    RoleId = authDTO.RoleId,

                };

                await employeeRespository.AddEmployee(data);
            }
            else
            {
                return BadRequest(ModelState);
            }

            var employeeId = await employeeRespository.GetEmployeeByEmail(authDTO.Email);

            var identityUser = new IdentityUser { 
            
                Email = authDTO.Email,
                UserName = "EMP" + employeeId,
            };

            var result = await _userManager.CreateAsync(identityUser, authDTO.Password);

            if (result.Succeeded)
            {
                //roles need to be added
                return Ok($"User has been registered! Username is EMP{employeeId}");
            }
            else
            {
                await employeeRespository.DeleteEmployeeById(employeeId.Value);

                return BadRequest("User has NOT been registered!");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginDTO authDTO)
        {
            if (authDTO.Username == null || authDTO.Password == null)
            {

                return BadRequest("Empty Parameters");
            }
            else {

                var user = await _userManager.FindByNameAsync(authDTO.Username);

                if (user == null)
                {
                    return BadRequest("User was not found!!");
                }
                else
                {
                    var result = await _userManager.CheckPasswordAsync(user, authDTO.Password);

                    if (result)
                    {
                        //Token
                        var JwtToken = jwtTokenRepo.CreateJWTToken(user);
                        return Ok(JwtToken);
                    }
                }
                return BadRequest("User was not found!!");
            }
        }

    }
}