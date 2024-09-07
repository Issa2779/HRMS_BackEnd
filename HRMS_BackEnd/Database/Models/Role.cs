using System;
using System.Collections.Generic;

namespace HRMS_BackEnd.Database.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<RolesChangeRequest> RolesChangeRequestNewRoles { get; set; } = new List<RolesChangeRequest>();

    public virtual ICollection<RolesChangeRequest> RolesChangeRequestOldRoles { get; set; } = new List<RolesChangeRequest>();
}
