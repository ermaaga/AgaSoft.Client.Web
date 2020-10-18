using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AgaSoft.Client.Model.Entities
{
    public class UserRoles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Role { get; set; }

        public List<Users> Users { get; } = new List<Users>();
    }
}
