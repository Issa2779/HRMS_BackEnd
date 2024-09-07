using HRMS_BackEnd.Database.Context;
using HRMS_BackEnd.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

namespace HRMS_BackEnd.Repositories.EmployeeRepository
{
    public class SqlEmployeeRepository : IEmployeeRespository
    {

        private readonly HrmsDbContext hrmsContext;
        private readonly ILogger<SqlEmployeeRepository> logger;

        public SqlEmployeeRepository(HrmsDbContext hrmsContext, ILogger<SqlEmployeeRepository> logger)
        {
            this.hrmsContext = hrmsContext;
            this.logger = logger;
        }

        //Get Certain Employee
        public async Task<Employee?> GetEmployeeById(int id)
        {
            try
            {
                var data = await hrmsContext.Employees.FindAsync(id);

                return (data != null) ? data : null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        //Get All Employees
        public async Task<List<Employee>?> GetAllEmployees(int pageNumber, int pageSize)
        {
            try
            {
                int skip = (pageNumber - 1) * pageSize;
                var entityData = await hrmsContext.Employees.Skip(skip).Take(pageSize).ToListAsync();

                return (entityData != null) ? entityData : null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                
            }

        }

        public async Task AddEmployee(Employee data)
        {
            try
            {

                await hrmsContext.AddAsync(data);
                logger.LogDebug("\n\n\n\n\n\n\nIssue here with Save Changes\n\n\n\n\n");
                await hrmsContext.SaveChangesAsync();

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        }

        public async Task UpdateEmployee(Employee data)
        {

            try
            {
                var entityData = await hrmsContext.Employees.FirstOrDefaultAsync(l => l.EmployeeId == data.EmployeeId);

                logger.LogInformation($"\n\n\n\n\n\n{entityData}\n\n\n\n\n\n");

                if (entityData != null)
                {
                    entityData.Phone = data.Phone;
                    await hrmsContext.SaveChangesAsync();
                }
                else
                    throw new Exception("No Data is found");

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<int?> GetEmployeeByEmail(string email)
        {
            try
            {
                var data = await hrmsContext.Employees.Where(l => l.Email == email).Select(l => l.EmployeeId).FirstOrDefaultAsync();


                return data;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteEmployeeById(int id)
        {
            try
            {
                var data = await hrmsContext.Employees.FindAsync(id);

                if(data != null)
                {
                    hrmsContext.Employees.Remove(data);
                    await hrmsContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("User has NOT been Regestired!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}