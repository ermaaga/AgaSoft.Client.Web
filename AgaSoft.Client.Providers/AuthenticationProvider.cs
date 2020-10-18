using AgaSoft.Client.Interfaces;
using AgaSoft.Client.Model;
using AgaSoft.Client.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AgaSoft.Client.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private DbContextOptions<AgaSoftRepositoryContext> _option;
        public AuthenticationProvider(DbContextOptions<AgaSoftRepositoryContext> option)
        {
            _option = option;
        }
        bool IAuthenticationProvider.Register(string email, string password, out string message)
        {
            bool result = true;
            message = string.Empty;
            try
            {
                using (AgaSoftRepositoryContext _DbContext = new AgaSoftRepositoryContext(_option))
                {
                    Users newUser = new Users();
                    //newUser.Id = _DbContext.Users.Max(x => x.Id) + 1;
                    newUser.Id = 1;
                    newUser.Email = email;
                    newUser.Password = password;

                    _DbContext.Users.Add(newUser);
                    _DbContext.SaveChanges();                    
                }

            }
            catch (Exception ex)
            {
                result = false;
                message = $"{ex.Message}";
            }
            return result;
        }
    }
}
