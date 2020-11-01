using System;

namespace AgaSoft.Client.Interfaces
{
    public interface IAuthenticationProvider
    {
        bool Register(string name, string lastname, string username, string email, string password, string description, out string message);
        bool Login(string email, string password, out string message);
    }
}
