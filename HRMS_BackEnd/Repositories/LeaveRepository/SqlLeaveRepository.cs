using Microsoft.EntityFrameworkCore;
using HRMS_BackEnd.Repositries.LeaveRepository;
using HRMS_BackEnd.Repositries.LeaveRepositry;
using HRMS_BackEnd.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using HRMS_BackEnd.Database.Models;
using HRMS_BackEnd.Database.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace HRMS_BackEnd.Repositries.LeaveRepository
{
    public class SqlLeaveRepository : ILeaveRepository
    {

        private readonly HrmsDbContext hrmsDbContext;

        public SqlLeaveRepository(HrmsDbContext context)
        {
            hrmsDbContext = context;

        }

        //GetAllLeaveRequests for employee
        public async Task<List<LeaveRequest>?> GetLeaveRequestEmployee(int id)
        {

            try
            {
                var data = await hrmsDbContext.LeaveRequests.Where(s => s.EmployeeId == id).ToListAsync();

                return (data != null) ? data : null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //Check Dates for leave request 
        private async Task<bool> CheckDatesByEId(int id,DateOnly start, DateOnly end)
        {


            //select startDate, EndDate from LeaveRequests where EmployeeId = id;
            List<LeaveRequest> datesLeaves = await hrmsDbContext.LeaveRequests.Where(l => l.EmployeeId == id)
                .Select(l => new LeaveRequest
                {
                    StartDate = l.StartDate,
                    EndDate = l.EndDate

                }).ToListAsync();


            if(datesLeaves == null)
            {
                return false;
            }

            foreach (var item in datesLeaves)
            {
                if (start <= item.EndDate && end >= item.StartDate)
                {
                   
                    return false;
                }
            }
            return true;

        }


        //Registering a leave request
        public async Task<string> AddLeaveRequest(LeaveRequest postData)
        {


            bool check = await CheckDatesByEId(postData.EmployeeId, postData.StartDate, postData.EndDate);


            try
            {
                if(check)
                {
                    await hrmsDbContext.LeaveRequests.AddAsync(postData);
                    await hrmsDbContext.SaveChangesAsync();
                    return "Request has been added and waiting for approval";
                }
                else
                {
                    return "Request cannot be added as it conflicts with another leave date";
                }
                
            }
            catch (Exception e)
            {

                return "Error: Data has not been added to the database: " + e;

            }

            

        }

        //Removing a leave request
        public async Task<string> RemoveLeaveRequest(LeaveRequest postData) 
        {

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            try { 

                var withdraw = await hrmsDbContext.LeaveRequests
                   .FirstOrDefaultAsync(lr => lr.EmployeeId == postData.EmployeeId && lr.StartDate == postData.StartDate);


                //Carefull as it needs to be sent as what is intended startdate from the user
                if (withdraw == null)
                {
                    return "Not Found";
                }
                else if(withdraw.StartDate < currentDate)
                {
                    return "Current";
                }
                else
                {
                    hrmsDbContext.LeaveRequests.Remove(withdraw);

                    await hrmsDbContext.SaveChangesAsync();

                    return "Committed";
                }

            }
            catch(Exception e)
            {
                return $"{e}";
            }
          
        }

        //Detuct Range
        public async Task DetuctLeaveRequestRep(LeaveRequest requestData, DateOnly cancelledFrom, DateOnly cancelledTo)
        {
            //select * from leaves where employee_ID = id and start_date = "start" 
            try
            {
                var dataEntity = await hrmsDbContext.LeaveRequests.FirstOrDefaultAsync(l => l.EmployeeId == requestData.EmployeeId && l.StartDate == requestData.StartDate);

                if (dataEntity == null || dataEntity.StartDate != requestData.StartDate || dataEntity.EndDate != requestData.EndDate)
                {
                    return;
                }
                else
                {
                    if (dataEntity.StartDate != cancelledFrom && dataEntity.EndDate == cancelledTo)
                    {
                        //Update
                        dataEntity.EndDate = cancelledFrom;
                        await hrmsDbContext.SaveChangesAsync();
                    }
                    else if (dataEntity.StartDate == cancelledFrom && dataEntity.EndDate != cancelledTo)
                    {

                        //Update

                        dataEntity.StartDate = cancelledTo;
                        await hrmsDbContext.SaveChangesAsync();


                    }
                    else if (dataEntity.StartDate != cancelledFrom && dataEntity.EndDate != cancelledTo)
                    {

                        int tempEmployee = dataEntity.EmployeeId;
                        DateOnly tempStart = dataEntity.StartDate;
                        DateOnly tempEnd = dataEntity.EndDate;
                        string? tempReason = dataEntity.Reason;
                        string? tempStatus = dataEntity.Status;

                        hrmsDbContext.LeaveRequests.Remove(dataEntity); await hrmsDbContext.SaveChangesAsync();

                        LeaveRequest postData = new LeaveRequest
                        {
                            EmployeeId = tempEmployee,
                            LeaveType = "Test",
                            StartDate = tempStart,
                            EndDate = cancelledFrom.AddDays(-1),
                            Reason = tempReason,
                            Status = tempStatus
                        };

                        //Insert
                        await hrmsDbContext.AddAsync(postData);
                        await hrmsDbContext.SaveChangesAsync();

                        postData = new LeaveRequest
                        {
                            EmployeeId = tempEmployee,
                            LeaveType = "Test",
                            StartDate = cancelledTo.AddDays(+1),
                            EndDate = tempEnd,
                            Reason = tempReason,
                            Status = tempStatus
                        };

                        //Insert

                        await hrmsDbContext.AddAsync(postData);
                        await hrmsDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        //Delete
                        hrmsDbContext.LeaveRequests.Remove(dataEntity);
                        await hrmsDbContext.SaveChangesAsync();

                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
