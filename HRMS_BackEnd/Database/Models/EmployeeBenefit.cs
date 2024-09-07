using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class EmployeeBenefit
{
    public int EmployeeBenefitId { get; set; }

    public int EmployeeId { get; set; }

    public int BenefitId { get; set; }

    public DateOnly EnrollmentDate { get; set; }

    public virtual Benefit Benefit { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
