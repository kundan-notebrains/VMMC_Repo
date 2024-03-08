using System.ComponentModel.DataAnnotations;

namespace VMMC.Models.CommanModeles
{
    public class UserRefreshTokens
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? UserId {get; set; }
        public string? name { get; set; }
        public string? Emailaddress { get; set; }
        [Required]
        public string? TokenKey { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
