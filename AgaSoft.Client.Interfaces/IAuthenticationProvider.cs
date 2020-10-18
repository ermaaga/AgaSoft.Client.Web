using System;

namespace AgaSoft.Client.Interfaces
{
    public interface IAuthenticationProvider
    {
        bool Register(string email, string password, out string message);

    }
}
