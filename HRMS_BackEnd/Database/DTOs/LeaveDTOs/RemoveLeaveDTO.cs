using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.LeaveDTOs
{
    public class RemoveLeaveDTO
    {

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

    }
}
