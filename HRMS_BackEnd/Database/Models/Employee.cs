using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class Employee
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

    public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; } = new List<EmployeeBenefit>();

    public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<RolesChangeRequest> RolesChangeRequests { get; set; } = new List<RolesChangeRequest>();
}
