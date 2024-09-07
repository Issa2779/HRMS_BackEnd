using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class Evaluation
{
    public int EvaluationId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly EvaluationDate { get; set; }

    public string Evaluator { get; set; } = null!;

    public int? Score { get; set; }

    public string? Comments { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
