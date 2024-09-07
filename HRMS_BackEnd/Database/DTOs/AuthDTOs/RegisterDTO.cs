using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
        
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Phone { get; set; }
        [Required]
        public DateOnly HireDate { get; set; }
        [Required]
        public string? Position { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
        [Required]
        public int? RoleId { get; set; }

    }
}
