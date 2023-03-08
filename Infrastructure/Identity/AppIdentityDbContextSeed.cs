using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            try
            {
                if (dbContext.Database.IsSqlServer())
                {
                    dbContext.Database.Migrate();
                }

                var role = new IdentityRole()
                {
                    Name = "Super Admin"
                };

                await roleManager.CreateAsync(role);

                var user = new ApplicationUser
                {
                    UserName = "SuperAdmin"
                };

                var result= await userManager.CreateAsync(user, "Admin@123");
                if(result.Succeeded)
                {
                    var adminUser = await userManager.FindByNameAsync("SuperAdmin");
                    await userManager.AddToRoleAsync(adminUser, role.Name);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
