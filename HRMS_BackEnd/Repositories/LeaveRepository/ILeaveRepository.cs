using HRMS_BackEnd.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_BackEnd.Repositries.LeaveRepositry
{
    public interface ILeaveRepository
    {


        public Task<List<LeaveRequest>?> GetLeaveRequestEmployee(int id);

        public Task<string> AddLeaveRequest(LeaveRequest postData);

        public Task<string> RemoveLeaveRequest(LeaveRequest postData);

        public Task DetuctLeaveRequestRep(LeaveRequest requestData, DateOnly start, DateOnly To);


    }
}
