using HRMS_BackEnd.Database.Context;
using HRMS_BackEnd.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS_BackEnd.Repositories.Attendace_Repository
{
    public class SqlAttendanceRepository : IAttendanceRepository
    {

        private readonly HrmsDbContext context;
        private readonly ILogger<SqlAttendanceRepository> logger;

        public SqlAttendanceRepository(HrmsDbContext context, ILogger<SqlAttendanceRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task CheckInAttendanceRequest(AttendanceRecord modelAttendanceData)
        {

            try
            {
                await context.AttendanceRecords.AddAsync(modelAttendanceData);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task CheckOutAttendanceRequest(AttendanceRecord modelAttendanceData)
        {
            try
            {
                
                DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);

                var entityData = await context.AttendanceRecords.Where(l => l.EmployeeId == modelAttendanceData.EmployeeId && l.AttendanceDate == dateNow)
                    .FirstOrDefaultAsync();


                if(entityData != null) {

                    entityData.CheckOutTime = modelAttendanceData.CheckOutTime;

                    if (entityData.CheckInTime.HasValue && modelAttendanceData.CheckOutTime.HasValue)
                    {
                        TimeOnly start = entityData.CheckInTime.Value; //To check for Nullable Data
                        TimeOnly end = modelAttendanceData.CheckOutTime.Value; 
                        TimeSpan timeDifference = end.ToTimeSpan() - start.ToTimeSpan();

                        entityData.Remarks = timeDifference.ToString();

                        if (timeDifference >= TimeSpan.FromHours(8))
                        {
                            entityData.Status = "Full Attendance";
                        }
                        else
                        {
                            entityData.Status = "Partial Attendance";
                        }

                    }
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("No Data Found");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An Error has Occured" + ex.Message);
            }
        }
    }
}