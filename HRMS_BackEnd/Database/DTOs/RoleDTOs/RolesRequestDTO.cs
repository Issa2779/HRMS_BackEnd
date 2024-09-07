using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.RoleDTOs
{
    public class RolesRequestDTO
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string? RoleName { get; set; }

        [Required]
        public string? Reason { get; set; }  
    }
}
