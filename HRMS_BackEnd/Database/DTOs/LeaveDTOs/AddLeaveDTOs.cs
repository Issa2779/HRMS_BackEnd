using HRMS_BackEnd.Database.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace HRMS_BackEnd.Database.DTOs.LeaveDTOs
{

    //DTOs used to determine the data that needs to be added by the user
    public class AddLeaveDTOs
    {

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string LeaveType { get; set; } = null!;
        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        public string? Status { get; set; }
        public string? Reason { get; set; }


    }
}
