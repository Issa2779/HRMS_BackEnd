using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.AttendaceDTOs
{
    public class AttendanceCheckInOutRequestDTO
    {
        [Required]
        public int EmployeeId { get; set; }
    }
}
