using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class Benefit
{
    public int BenefitId { get; set; }

    public string BenefitName { get; set; } = null!;

    public string? Description { get; set; }

    public string? EligibilityCriteria { get; set; }

    public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; } = new List<EmployeeBenefit>();
}
