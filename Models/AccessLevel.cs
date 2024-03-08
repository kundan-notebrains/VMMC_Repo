using System;
using System.Collections.Generic;

namespace VMMC.Models;

public partial class AccessLevel
{
    public int Accessid { get; set; }

    public string? Levelname { get; set; }

    public bool? Levelstatus { get; set; }

    public string? Accesslevelid { get; set; }

    public bool? Isdeleted { get; set; }

    public bool? Status { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Updateddate { get; set; }
}
