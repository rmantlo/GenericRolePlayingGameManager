using System;
using System.Collections.Generic;
using System.Linq;
using ItemHoarder.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ItemHoarder.WebApi.Startup))]

namespace ItemHoarder.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
            MakeRoles();
        }

        private void MakeRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //creating admin role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var adminUser = new ApplicationUser();
                adminUser.UserName = "admin";
                adminUser.Email = "admin@admin.com";
                string adminPWD = "Admin1!";
                var chkUser = UserManager.Create(adminUser, adminPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(adminUser.Id, "Admin");
                }
            }
            //creating GM role
            if (!roleManager.RoleExists("GameMaster"))
            {
                var role = new IdentityRole();
                role.Name = "GameMaster";
                roleManager.Create(role);
            }
            //creating Player role
            if (!roleManager.RoleExists("Player"))
            {
                var role = new IdentityRole();
                role.Name = "Player";
                roleManager.Create(role);
            }
        }
    }

}
