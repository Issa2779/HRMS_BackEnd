﻿using System.ComponentModel.DataAnnotations;

namespace HRMS_BackEnd.Database.DTOs.EmployeeDTOs
{
    public class EmployeeDataRequest
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
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
