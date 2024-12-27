using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingSystem.Domain.Core;

namespace AccountingSystem.Domain.Users
{
    public class User : Entity<Guid>
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public string Email { get; private set; }

        public List<string> Roles { get;  set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public User() { }
        public User(Guid id) : base(id) { }

        public User(string login, string password, string salt, string email, string name, string surname)
        {
            Login = login;
            Password = password;
            Email = email;
            Roles = new List<string>();
            Name = name;
            Surname = surname;
        }

        private Task ChangeAdmin(bool isAdmin)
        {
            if (isAdmin)
            {
                Roles.Add("admin");
            }
            else
            {
                Roles.Remove("admin");
            }
            
            return Task.CompletedTask;
        }

        public Task OnAdminStatus() => ChangeAdmin(true);
        public Task OffAdminStatus() => ChangeAdmin(false);
    }
}