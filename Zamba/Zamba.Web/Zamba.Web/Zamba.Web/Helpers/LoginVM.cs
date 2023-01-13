namespace Zamba.Web.Helpers
{
    internal class LoginVM
    {
        public LoginVM()
        {
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ComputerNameOrIp { get; set; }
    }
}