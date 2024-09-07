using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class AttendanceRecord
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public string Status { get; set; } = null!;

    public TimeOnly? CheckInTime { get; set; }

    public TimeOnly? CheckOutTime { get; set; }

    public string? Remarks { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
