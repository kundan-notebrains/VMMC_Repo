namespace VMMC.IServices.ILogin
{
    public interface IHashingAndSalting
    {
        public string HashingPassword(string password, out byte[] salt);
        bool VerifyPassword(string password, string hash, byte[] salt);
    }
}
