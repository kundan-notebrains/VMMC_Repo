using System;
using System.Collections.Generic;

namespace VMMC.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Rolename { get; set; }

    public bool? Rolestatus { get; set; }

    public string? Roleid { get; set; }

    public int? Moduleid { get; set; }

    public bool? Isdeleted { get; set; }

    public bool? Status { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Updateddate { get; set; }
}
