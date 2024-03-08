namespace VMMC.Models.CommanModeles
{
    public class LoginModel
    {
        public int? id { get; set; }
        public string UserId { get; set; } = null!;
        public string? Password { get; set; }
        public bool? isactive { get; set; }
        public bool? isdeleted { get; set; }
    }
}
