using System;
using System.Collections.Generic;

namespace VMMC.Models;

public partial class Systemlogin
{
    public int Id { get; set; }

    public string? Userid { get; set; }

    public string? Password { get; set; }

    public string? Refreshtoken { get; set; }

    public bool? Isdeleted { get; set; }

    public bool? Status { get; set; }
}
