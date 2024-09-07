using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.EmployeeDTOs
{
    public class UpdateEmployeeRequest
    {

        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string? Phone { get; set; }

    }
}
