using System;
using System.Collections.Generic;
using System.Text;

namespace AgaSoft.Client.Model.Entities
{

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }

    }
}
