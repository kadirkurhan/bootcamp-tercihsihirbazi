using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TercihSihirbazi.Business.Concrete;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.Business.ValidationRules.FluentValidation;
using TercihSihirbazi.Data.Concrete.EntityFrameworkCore.Repositories;
using TercihSihirbazi.Data.Interfaces;
using TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using TercihSihirbazi.DataAccess.Interfaces;
using TercihSihirbazi.Entities.Dtos.AppUserDtos;
using TercihSihirbazi.Entities.Dtos.ProfileDtos;

namespace TercihSihirbazi.Business.Containers.MicrosoftIoc
{
    public static class CustomExtension
    {

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));

            services.AddScoped<IProfileDal, EfProfileRepository>();
            services.AddScoped<IProfileService, ProfileManager>();

            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<IAppUserService, AppUserManager>();

            services.AddScoped<IAppRoleDal, EfAppRoleRepository>();
            services.AddScoped<IAppRoleService, AppRoleManager>();

            services.AddScoped<IAppUserRoleDal, EfAppUserRoleRepository>();
            services.AddScoped<IAppUserRoleService, AppUserRoleManager>();

            services.AddScoped<IJwtService, JwtManager>();

            services.AddScoped<IExcelDataService,ExcelDataManager>();
            services.AddScoped<IExcelDAL, EfExcelDataRepository>();

            services.AddTransient<IValidator<ProfileAddDto>, ProfileAddDtoValidator>();
            services.AddTransient<IValidator<ProfileUpdateDto>, ProfileUpdateDtoValidator>();
            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();

            services.AddTransient<IValidator<AppUserAddDto>, AppUserAddDtoValidator>();
        }
    }

}
