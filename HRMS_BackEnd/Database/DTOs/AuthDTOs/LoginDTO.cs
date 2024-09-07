using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.AuthDTOs
{
    public class LoginDTO
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
