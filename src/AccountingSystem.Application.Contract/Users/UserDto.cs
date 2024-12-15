using System;

namespace AccountingSystem.Application.Contract.Users
{
    public class UserDto
    {
        public Guid Id { get;  set; }
        public string Login { get;  set; }

        public string Email { get;  set; }

        public bool IsAdmin { get;  set; }

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}