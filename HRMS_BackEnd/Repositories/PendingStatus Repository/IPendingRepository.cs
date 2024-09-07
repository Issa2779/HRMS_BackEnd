using HRMS_BackEnd.Database.Models;

namespace HRMS_BackEnd.Repositories.PendingStatus_Repository
{
    public interface IPendingRepository
    {

        Task<List<LeaveRequest>?> GetPendingLeaveRequests(int id);
        Task<string> ConfirmPendingLeaveRequest(int leaveRequestId, string statusConfirmation);

        Task<List<RolesChangeRequest>?> GetPendingRoleChangeRequests(int id);

        Task<string> ConfirmPendingRoleChangeRequest(int roleRequestId, string statusConfirmation, int employeeId);

    }
}
