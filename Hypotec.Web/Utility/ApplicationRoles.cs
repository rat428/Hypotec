using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Hypotec.Web.Utility
{
    public static class ApplicationRoles
    {
        public static void SeedAspNetRoles(RoleManager<IdentityRole> roleManager)
        {
            List<string> roleList = new List<string>()
            {
                "Admin",
                "SuperAdmin",
                "User",
                "ReportAdmin"
            };

            foreach (var role in roleList)
            {
                var result = roleManager.RoleExistsAsync(role).Result;
                if (!result)
                {
                    roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
