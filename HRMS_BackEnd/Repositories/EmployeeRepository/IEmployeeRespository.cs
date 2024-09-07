using HRMS_BackEnd.Database.DTOs.EmployeeDTOs;
using HRMS_BackEnd.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_BackEnd.Repositories.EmployeeRepository
{
    public interface IEmployeeRespository
    {

        public Task<Employee?> GetEmployeeById(int id);
        Task<List<Employee>?> GetAllEmployees(int pageNumber, int pageSize);
        Task AddEmployee(Employee data);

        Task UpdateEmployee(Employee data);


        Task<int?> GetEmployeeByEmail(string email);

        Task DeleteEmployeeById(int id);

    }
}
