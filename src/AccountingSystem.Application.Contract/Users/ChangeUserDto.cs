namespace AccountingSystem.Application.Contract.Users
{
    public class ChangeUserDto
    {
        public string Login { get;  set; }
        public string Password { get;  set; }

        public string Email { get;  set; }

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}