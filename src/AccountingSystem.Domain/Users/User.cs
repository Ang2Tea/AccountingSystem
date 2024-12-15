using System;
using AccountingSystem.Domain.Core;

namespace AccountingSystem.Domain.Users
{
    public class User : Entity<Guid>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        
        public User() {}
        public User(Guid id) : base(id) {}
    }
}