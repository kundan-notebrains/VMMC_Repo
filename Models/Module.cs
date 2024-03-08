using System;
using System.Collections.Generic;

namespace VMMC.Models;

public partial class Module
{
    public int Mid { get; set; }

    public string? Module1 { get; set; }

    public bool? Isdeleted { get; set; }

    public bool? Status { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Updateddate { get; set; }
}
