using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.Business.StringInfos;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.API.InitialMigrate
{
    public static class JwtIdentityInitializer
    {
        //private readonly IAppUserService _appUserService;
        //private readonly IAppUserRoleService _appUserRoleService;
        //private readonly IAppRoleService _appRoleService;
        //public JwtIdentityInitializer(IAppUserService appUserService, IAppUserRoleService appUserRoleService, IAppRoleService appRoleService)
        //{
        //    _appUserService = appUserService;
        //    _appUserRoleService = appUserRoleService;
        //    _appRoleService = appRoleService;

        //}
        //public JwtIdentityInitializer()
        //{

        //}

        //public async Task Seed()
        public static async Task Seed(IAppUserService appUserService, IAppUserRoleService appUserRoleService, IAppRoleService appRoleService)
        {
            //ilgili rol varmı?
            var adminRole = await appRoleService.FindByName(RoleInfo.Admin);
            if (adminRole == null)
            {
                await appRoleService.Add(new AppRole
                {
                    Name = RoleInfo.Admin
                });
            }

            var memberRole = await appRoleService.FindByName(RoleInfo.Member);
            if (memberRole == null)
            {
                await appRoleService.Add(new AppRole
                {
                    Name = RoleInfo.Member
                });
            }

            var adminUser = await appUserService.FindByUserName("kadir");
            if (adminUser == null)
            {
                await appUserService.Add(new AppUser
                {
                    FullName = "kadir kurhan",
                    UserName = "kadir",
                    Password = "1"
                });

                var role = await appRoleService.FindByName(RoleInfo.Admin);
                var admin = await appUserService.FindByUserName("kadir");

                await appUserRoleService.Add(new AppUserRole
                {
                    AppUserId = admin.Id,
                    AppRoleId = role.Id
                });
            }
        }
    }
}
