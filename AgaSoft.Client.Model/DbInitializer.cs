using System;
using System.Collections.Generic;
using AgaSoft.Client.Model.Entities;
using System.Text;
using System.Linq;

namespace AgaSoft.Client.Model
{
    public static class DbInitializer
    {
        public static void Initialize(AgaSoftContext context)
        {
            context.Database.EnsureCreated();


            if (!context.Users.Any())
            {
                var Users = new User[]
                {
                new User{Name="admin",LastName="admin",Email="admin",Username="admin",Password="admin",BirthDate = DateTime.Now,Sex="male"},
                new User{Name="Ermal",LastName="Halilaga",Email="ermal97@live.it",Username="Ermalhalilaga",Password="ermal",BirthDate = DateTime.Now,Sex="male"},
                new User{Name="Agostino",LastName="Abbattecola",Email="Agostinoxax@live.it",Username="Agostinoxax",Password="agostino",BirthDate = DateTime.Now,Sex="male"},
                new User{Name="Giuseppe",LastName="Boccuzzi",Email="giuseppe@live.it",Username="Giuseppe",Password="giuseppe",BirthDate = DateTime.Now,Sex="male"},
                };

                foreach (User u in Users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();

            }

            return;
        }
    }

}

