using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class TrainingProgram
{
    public int TrainingProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();
}
