using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class LeaveRequest
{
    public int LeaveRequestId { get; set; }

    public int EmployeeId { get; set; }

    public string LeaveType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Reason { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
