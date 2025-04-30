namespace Projet_mvc.Core.Domain
{
    public class User
    {
        public int User_Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password_Hash { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public DateTime Creation_Date { get; set; }
    }
}
