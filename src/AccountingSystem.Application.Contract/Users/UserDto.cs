using System;
using System.Collections.Generic;

namespace AccountingSystem.Application.Contract.Users
{
    public class UserDto
    {
        public Guid Id { get;  set; }
        public string Login { get;  set; }

        public string Email { get;  set; }

        public List<string> Roles { get;  set; }

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}