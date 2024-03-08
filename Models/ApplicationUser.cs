
using VMMC.Models.CommanModeles;

namespace VMMC.Models;

public partial class ApplicationUser
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Emailid { get; set; }

    public string? Phonenumber { get; set; }

    public int? Accesslevelid { get; set; }

    public string? Userid { get; set; }

    public string? Employeeno { get; set; }

    public DateTime? Dateofbirth { get; set; }

    public string? Officelocation { get; set; }

    public string? Password { get; set; }

    public bool? Isdeleted { get; set; }

    public bool? Status { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Updateddate { get; set; }

    public string? Token { get; set; } = null;
}
