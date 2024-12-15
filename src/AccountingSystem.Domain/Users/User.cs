using System;
using System.Threading.Tasks;
using AccountingSystem.Domain.Core;

namespace AccountingSystem.Domain.Users
{
    public class User : Entity<Guid>
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public string Email { get; private set; }

        public bool IsAdmin { get; private set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public User() { }
        public User(Guid id) : base(id) { }
        public User(string login, string password, string salt, string email, string name, string surname)
        {
            Login = login;
            Password = GeneratePasswordHash(password, salt);
            Email = email;
            IsAdmin = false;
            Name = name;
            Surname = surname;
        }

        private static string GeneratePasswordHash(string password, string salt)
        {
            return "";
        }
        
        private Task ChangeAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
            return Task.CompletedTask;
        }

        public Task OnAdminStatus() => ChangeAdmin(true);
        public Task OffAdminStatus() => ChangeAdmin(false);
        
        public async Task ChangePassword(string oldPassword, string newPassword, string salt)
        {
            var oldPasswordHash = Task.Run (() => GeneratePasswordHash(oldPassword, salt));
            var newPasswordHash = Task.Run(() => GeneratePasswordHash(newPassword, salt));
            
            var passwordHashes = await Task.WhenAll(oldPasswordHash, newPasswordHash);

            if (passwordHashes[0] != Password || passwordHashes[0] == passwordHashes[1])
            {
                throw new Exception("Passwords do not match");
            }
            
            Password = passwordHashes[1];
        }
    }
}