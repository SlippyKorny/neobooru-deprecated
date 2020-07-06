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
                NeobooruUser usr = new NeobooruUser()
                {
                    UserName = "root",
                    NormalizedUserName = "root".ToUpper()
                };
                IdentityResult res = userManager.CreateAsync(usr, "#RootPassword123").Result;
                if (res.Succeeded)
                    userManager.AddToRoleAsync(usr, "root").Wait();
            }
        }
    }
}