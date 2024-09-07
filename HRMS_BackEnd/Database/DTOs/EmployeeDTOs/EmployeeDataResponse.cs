namespace HRMS_BackEnd.Database.DTOs.EmployeeDTOs
{
    public class EmployeeDataResponse
    {

        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public DateOnly HireDate { get; set; }

        public string? Position { get; set; }

        public int? DepartmentId { get; set; }

        public int? RoleId { get; set; }

    }
}
