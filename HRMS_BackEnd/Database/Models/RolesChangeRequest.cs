using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class RolesChangeRequest
{
    public int RolesChangeRequestId { get; set; }

    public int EmployeeId { get; set; }

    public int OldRoleId { get; set; }

    public int NewRoleId { get; set; }

    public DateOnly RequestDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Reason { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Role NewRole { get; set; } = null!;

    public virtual Role OldRole { get; set; } = null!;
}
