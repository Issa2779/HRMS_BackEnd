using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.PendingDTO
{
    public class PendingLeaveRequestDTO
    {

        [Required]
        public int LeaveRequestId { get; set; }

        [Required]
        public string? ConfirmationStatus { get; set; }

    }
}
