using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.LeaveDTOs
{
    public class DetuctLeaveRequestDTO
    {
        [Required]
        public int EmployeeId { get; set; }

        public string LeaveType { get; set; } = null!;

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public DateOnly CancelledFromDate { get; set; }

        [Required]
        public DateOnly CancelledToDate { get; set; }


    }
}
