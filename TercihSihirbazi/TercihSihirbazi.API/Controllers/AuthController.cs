using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.Business.StringInfos;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Dtos.AppUserDtos;
using TercihSihirbazi.Entities.Token;
using TercihSihirbazi.WebApi.CustomFilters;

namespace TercihSihirbazi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        public AuthController(IJwtService jwtService, IAppUserService appUserService, IMapper mapper)
        {
            _mapper = mapper;
            _jwtService = jwtService;
            _appUserService = appUserService;
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> SignIn(AppUserLoginDto appUserLoginDto)
        {
            //?
            // userName =>  varmı 
            // password => eşleşiyor mu?
            //_jwtService.GenerateJwt()

            var appUser = await _appUserService.FindByUserName(appUserLoginDto.UserName);
            if (appUser == null)
            {
                return BadRequest("kullanıcı adı veya şifre hatalı");
            }
            else
            {
                if (await _appUserService.CheckPassword(appUserLoginDto))
                {
                    var roles = await _appUserService.GetRolesByUserName(appUserLoginDto.UserName);
                    var token = _jwtService.GenerateJwt(appUser, roles);
                    JwtAccessToken jwtAccessToken = new JwtAccessToken();
                    jwtAccessToken.Token = token;

                    var appUserRole = roles.FirstOrDefault();

                    AppUserAbilityDto ability = new()
                    {
                        action = "read",
                        subject = "Auth"
                    };
                    UserData userData = new UserData()
                    {
                        UserName = appUser.UserName,
                        ability = ability,
                        role = appUserRole.Name,
                        NickName = appUser.FullName

                    };

                    AppUserLoginWithTokenDto response = new AppUserLoginWithTokenDto()
                    {
                        userData = userData,
                        accessToken = token,
                        refreshToken = "",

                    };

                    return Created("", response);
                }
                return BadRequest("kullanıcı adı veya şifre hatalı");
            }
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> SignUp(AppUserAddDto appUserAddDto, [FromServices] IAppUserRoleService appUserRoleService, [FromServices] IAppRoleService appRoleService)
        {
            var appUser = await _appUserService.FindByUserName(appUserAddDto.UserName);
            if (appUser != null)
                return BadRequest($"{appUserAddDto.UserName} zaten alınmış");

            await _appUserService.Add(_mapper.Map<AppUser>(appUserAddDto));

            var user = await _appUserService.FindByUserName(appUserAddDto.UserName);
            var role = await appRoleService.FindByName(RoleInfo.Member);

            await appUserRoleService.Add(new AppUserRole
            {
                AppRoleId = role.Id,
                AppUserId = user.Id
            });


            List<AppRole> roles = new List<AppRole>(){
                new AppRole{
                    Id = role.Id,
                    Name = RoleInfo.Member
                }
            };

            var token = _jwtService.GenerateJwt(user, roles);
            AppUserAbilityDto ability = new()
            {
                action = "read",
                subject = "Auth"
            };
            UserData userData = new UserData()
            {
                UserName = user.UserName,
                ability = ability,
                role = RoleInfo.Member,
                NickName = user.FullName
            };

            AppUserLoginWithTokenDto response = new AppUserLoginWithTokenDto()
            {
                userData = userData,
                accessToken = token,
                refreshToken = ""
            };

            //await SignIn(appUserAddDto.UserName, appUserAddDto.Password);
            return Created("", response);
        }



        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> ActiveUser()
        {
            var user = await _appUserService.FindByUserName(User.Identity.Name);
            var roles = await _appUserService.GetRolesByUserName(User.Identity.Name);
            
            AppUserDto appUserDto = new AppUserDto
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Roles = roles.Select(I => I.Name).ToList()
            };

            return Ok(appUserDto);
        }

    }
}