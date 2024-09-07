using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.PendingDTO
{
    public class PendingRoleChangeRequest
    {

        [Required]
        public int RoleRequestId { get; set; }

        [Required]
        public int EmployeeId { get; set; } 
        [Required]
        public string? ConfirmationStatus { get; set; }
    }
}
