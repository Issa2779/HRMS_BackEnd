using HRMS_BackEnd.Database.Context;
using HRMS_BackEnd.Database.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace HRMS_BackEnd.Repositories.RolePosition_Repository
{
    public class SqlRoleRepository : IRoleRepository
    {

        private readonly HrmsDbContext context;
        private readonly ILogger<SqlRoleRepository> logger;
        private string pending = "Pending";

        public SqlRoleRepository(HrmsDbContext context, ILogger<SqlRoleRepository> logger)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<string> RoleRequestChange(int employeeId ,string roleNameNew, string? reason)
        {

            try
            {
                var employeeData = await context.Employees.FindAsync(employeeId); //Employee Data

                if (employeeData != null)
                {
                    var roleIdNew = await context.Roles.Where(l => l.RoleName == roleNameNew).Select(l => l.RoleId).FirstOrDefaultAsync(); //New Role Id

                    var roleIdOld = await context.Roles.Where(l => l.RoleName == employeeData.Position).Select(l => l.RoleId).FirstOrDefaultAsync(); //Old Role ID

                    DateOnly today = DateOnly.FromDateTime(DateTime.Now);


                    var roleChangeEntity = new RolesChangeRequest
                    {

                        EmployeeId = employeeData.EmployeeId,
                        OldRoleId = roleIdOld,
                        NewRoleId = roleIdNew,
                        RequestDate = today,
                        Status = pending,
                        Reason = reason,

                    };

                    await context.RolesChangeRequests.AddAsync(roleChangeEntity);
                    await context.SaveChangesAsync();

                    return "Request has been added and it is pending confirmation";
                }
                else
                {
                    throw new Exception("An error has Occured");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
          
        }



    }
}
