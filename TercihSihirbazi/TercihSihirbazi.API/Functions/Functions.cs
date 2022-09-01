using AutoMapper;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.Entities.Dtos.AppUserDtos;
namespace TercihSihirbazi.API.Functions
{
    public class Functions
    {

        private readonly IJwtService _jwtService;
        private readonly IAppUserService _appUserService;
        public Functions(IJwtService jwtService, IAppUserService appUserService)
        {
            _jwtService = jwtService;
            _appUserService = appUserService;
        }
        public Functions()
        {

        }

        public async Task<AppUserDto> ActiveUser(string Name)
        {
            var user = await _appUserService.FindByUserName(Name);
            var roles = await _appUserService.GetRolesByUserName(Name);

            AppUserDto appUserDto = new AppUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Roles = roles.Select(I => I.Name).ToList()
            };

            return appUserDto;
        }
    }
}