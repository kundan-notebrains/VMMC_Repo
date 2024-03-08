using Swashbuckle.AspNetCore.SwaggerGen;

namespace VMMC.Models.CommanModeles
{
    public class Users
    {
        public int Userid { get; set; }

        public string? Username { get; set; }

        public string? Usersurname { get; set; }

        public string? Useremailid { get; set; }
        public int? Useraccesslevelid { get; set; }
        public string? Userphonenumber { get; set; }
        public Dictionary<string, int>? Usermoduleid { get; set; }
        public Dictionary<string, List<int>?>? Userroleid { get; set; }     

        public string? Useruserid { get; set; }

        public string? Useremployeeno { get; set; }

        public DateTime? Userdateofbirth { get; set; }

        public string? Userofficelocation { get; set; }

        public string? Userpassword { get; set; }

       
    }
    //public class roleIDmodule
    //{
    //    public List<string>? rolekey { get; set; }
    //    public List<int>? rolesId { get; set; }
        
    //}
    //public class modulesID
    //{
    //    public List<string>? modulekey { get; set; }
    //    public List<int>? moulesId { get; set; }
       
    //}
}
