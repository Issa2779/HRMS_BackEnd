using HRMS_BackEnd.Database.Context;
using HRMS_BackEnd.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS_BackEnd.Repositories.PendingStatus_Repository
{
    public class SqlPendingStatusHandler : IPendingRepository
    {

        private readonly HrmsDbContext context;
        private readonly ILogger<SqlPendingStatusHandler> logger;
        
        private string employeePending = "Pending";

        private string[] checkStatus = { "approved" , "rejected" };
        

        public SqlPendingStatusHandler(HrmsDbContext context, ILogger<SqlPendingStatusHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<List<LeaveRequest>?> GetPendingLeaveRequests(int id)
        {

            try
            {
                var entityData = await context.LeaveRequests.Where(l => l.Status == employeePending && l.EmployeeId == id).ToListAsync();

                return (entityData != null) ? entityData : null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<string> ConfirmPendingLeaveRequest(int leaveRequestId, string statusConfirmation)
        {

            
            var entityData = await context.LeaveRequests.FindAsync(leaveRequestId);


            if (entityData != null && (statusConfirmation == checkStatus[0] || statusConfirmation == checkStatus[1]))
            {
                entityData.Status = statusConfirmation;
                await context.SaveChangesAsync();
                return "Request has been " + entityData.Status;

            }
            else
            {
                throw new Exception("An Error has Occured");
            }

        }
                                                                                                                                                                                                                                                    
        public async Task<List<RolesChangeRequest>?> GetPendingRoleChangeRequests(int id)
        {
            try
            {
                var entityData = await context.RolesChangeRequests.Where(l => l.Status == employeePending && l.EmployeeId == id).ToListAsync();

                return (entityData != null) ? entityData : null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ConfirmPendingRoleChangeRequest(int roleRequestId, string statusConfirmation, int employeeId)
        {
            var entityData = await context.RolesChangeRequests.FindAsync(roleRequestId);


            if (entityData != null && (statusConfirmation == checkStatus[0] || statusConfirmation == checkStatus[1]))
            {
                entityData.Status = statusConfirmation;
                await context.SaveChangesAsync();


                if(statusConfirmation  == checkStatus[1])
                {
                    return "Request has been " + entityData.Status;
                }
                else
                {
                    var entityEmployee = await context.Employees.FindAsync(employeeId);
                    var entityRole = await context.Roles.FindAsync(entityData.NewRoleId);

                    if (entityEmployee != null && entityRole != null)
                    {
                        entityEmployee.RoleId = entityData.NewRoleId;

                        entityEmployee.Position = entityRole.RoleName;

                        await context.SaveChangesAsync();

                        return "Request has been " + entityData.Status;
                    }
                    else
                        throw new Exception("An issue in data being NULL");
                }

            }
            else
            {
                throw new Exception("An Error has Occured");
            }
        }

    }
}