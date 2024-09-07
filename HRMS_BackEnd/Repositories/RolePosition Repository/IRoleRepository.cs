using HRMS_BackEnd.Database.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace HRMS_BackEnd.Repositories.RolePosition_Repository
{
    public interface IRoleRepository
    {

        Task<string> RoleRequestChange(int employeeId, string roleName, string? reason);

    }
}
