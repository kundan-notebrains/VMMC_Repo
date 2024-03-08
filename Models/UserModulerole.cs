using System;
using System.Collections.Generic;

namespace VMMC.Models;

public partial class UserModulerole
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? Moduleid { get; set; }

    public int? Accessroleid { get; set; }
}
