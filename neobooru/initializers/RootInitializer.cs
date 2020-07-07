using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using neobooru.Models;

namespace neobooru.seeds
{
    public class RootInitializer
    {
        public static void SeedUser(UserManager<NeobooruUser> userManager)
        {
            if (userManager.FindByNameAsync("root").Result == null)
            {
                NeobooruUser user = new NeobooruUser()
                {
                    UserName = "root",
                    NormalizedUserName = "root".ToUpper()
                };
                IdentityResult res = userManager.CreateAsync(user, "#RootPassword123").Result;
            }
            
            NeobooruUser usr = userManager.FindByNameAsync("root").Result;
            if (usr != null)
                userManager.AddToRoleAsync(usr, "root").Wait();
        }
    }
}