using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class EmployeeTraining
{
    public int EmployeeTrainingId { get; set; }

    public int EmployeeId { get; set; }

    public int TrainingProgramId { get; set; }

    public DateOnly EnrollmentDate { get; set; }

    public DateOnly? CompletionDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual TrainingProgram TrainingProgram { get; set; } = null!;
}
