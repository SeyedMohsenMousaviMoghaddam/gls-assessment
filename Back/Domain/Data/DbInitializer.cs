using DAL.DataBaseContexts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GLSTablesDataBaseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            var roles = new Role[]
            {
            new Role{Name=Infra.UserType.Admin,Description="Admin",CreatedOn=DateTime.Parse("2020-09-01")},
            };
            foreach (Role role in roles)
            {
                context.Roles.Add(role);
            }
            context.SaveChanges();

            var users = new User[]
            {
            new User{Name="Chemistry",Roles = roles.ToList()},
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }
    }
}
