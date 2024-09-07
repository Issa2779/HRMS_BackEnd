using HRMS_BackEnd.Database.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HRMS_BackEnd.Repositories.Attendace_Repository
{
    public interface IAttendanceRepository
    {

        Task CheckInAttendanceRequest(AttendanceRecord modelAttendanceData);

        Task CheckOutAttendanceRequest(AttendanceRecord modelAttendanceData);
    }
}
