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
        bool IAuthenticationProvider.Register(string email, string username, string password, out string message)
        {
            bool result = true;
            message = string.Empty;
            try
            {
                bool result = true;
                message = string.Empty;
                try
                {
                    if ((name != null && name != string.Empty) &&
                        (lastname != null && lastname != string.Empty) &&
                        (username != null && username != string.Empty) &&
                        (email != null && email != string.Empty) &&
                        (password != null && password != string.Empty) &&
                        (description != null && description != string.Empty))
                    {
                        using (AgaSoftRepositoryContext _DbContext = new AgaSoftRepositoryContext(_option))
                        {

                            if (!_DbContext.Users.Any(x => x.Username.Equals(username) || x.Email.Equals(username)))
                            {
                                User newUser = new User();
                                newUser.Name = name;
                                newUser.LastName = lastname;
                                newUser.Username = username;
                                newUser.Email = email;
                                newUser.Password = password;
                                newUser.Description = description;

                                _DbContext.Users.Add(newUser);
                                _DbContext.SaveChanges();


                                message = "Registrazione avvenuta con successo!";
                            }
                            else
                            {
                                result = false;
                                message = $"Registrazione negata, l'utente {username} è già presente";
                            }
                        }
                    }
                    else
                    {
                        result = false;
                        message = "Alcuni campi non sono stati compilati, la registrazione non può essere effettuata.";
                    }

                }
            catch (Exception ex)
            {
                result = false;
                message = $"{ex.Message}";
            }
            return result;
        }
        bool IAuthenticationProvider.Login(string username, string password, out string message)
        {
            bool result = true;
            message = string.Empty;
            try
            {
                if ((username != null && username != string.Empty) && (password != null && password != string.Empty))
                {

                    using (AgaSoftRepositoryContext _DbContext = new AgaSoftRepositoryContext(_option))
                    {
                        if (_DbContext.Users.Any(x => x.Username.Equals(username) && x.Password.Equals(password) || x.Email.Equals(username) && x.Password.Equals(password)))
                        {
                            result = true;
                            message = "Login effettuato con successo!";
                        }
                        else
                        {
                            result = false;
                            message = "Le credenziali inserite non sono corrette oppure l'utente non è registrato.";
                        }
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
