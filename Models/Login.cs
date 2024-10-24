namespace Technical.Models
{
    public class Login
    {
        public string StudentUserName { get; set; }

        public string Password { get; set; }


        public Login()
        {
            this.StudentUserName = "Unknown";
            this.Password = "Unknown";
        }
    }
}
