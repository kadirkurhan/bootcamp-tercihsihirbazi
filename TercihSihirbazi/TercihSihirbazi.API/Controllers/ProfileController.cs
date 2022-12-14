using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TercihSihirbazi.API.Functions;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.Business.StringInfos;
using TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Context;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Dtos.ProfileDtos;
using TercihSihirbazi.WebApi.CustomFilters;

namespace TercihSihirbazi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IExcelDataService _excelDataService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        public ProfileController(IProfileService profileService, IMapper mapper, IExcelDataService excelDataService, IAppUserService appUserService)
        {
            _mapper = mapper;
            _profileService = profileService;
            _excelDataService = excelDataService;
            _appUserService = appUserService;
        }

        //api/profile
        [HttpGet]
        [Authorize(Roles = RoleInfo.Admin + "," + RoleInfo.Member)]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _profileService.GetAll();
            return Ok(profiles);
        }

        //api/profile/3
        //ValidProfileId ValidAppUserId  ValidId<Profile> ValidId<AppUser>
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Entities.Concrete.Profile>))]
        [Authorize(Roles = RoleInfo.Admin)]
        public async Task<IActionResult> GetById(int id)
        {
            var profile = await _profileService.GetById(id);
            return Ok(profile);
        }

        [HttpPost]
        [Authorize(Roles = RoleInfo.Admin)]
        [ValidModel]
        public async Task<IActionResult> Add(ProfileAddDto profileAddDto)
        {
            await _profileService.Add(_mapper.Map<Entities.Concrete.Profile>(profileAddDto));
            return Created("", profileAddDto);

        }
        // [Authorize]
        // [HttpGet]
        // [Route("AddAsMember")]
        // public async Task<IActionResult> AddAsMember(int id)
        // {
        //     Functions functions = new Functions();
        //     //var activeUser = await functions.ActiveUser(User.Identity.Name);
        //     var kadirUser = await _appUserService.FindByUserName("test@test.com");
        //     TercihSihirbaziContext dbcontext = new TercihSihirbaziContext();
        //     ProfileFavoriteDto favoriteDto = new ProfileFavoriteDto()
        //     {
        //         AppUserId = kadirUser.Id,
        //         DetailObjectId = id
        //     };
        //     dbcontext.AppUserFavorites.AddAsync(_mapper.Map<AppUserFavorites>(favoriteDto));
        //     dbcontext.SaveChangesAsync();
        //     AppUser findedUser = await dbcontext.AppUsers.Include(i => i.AppUserFavorites).Include(i => i.AppUserRoles).Where(i => i.Id == 1).FirstOrDefaultAsync();

        //     var findedBolum = dbcontext.ExcelData.Include(i => i.FavoritedAppUsers).Where(i => i.Id == 12836).FirstOrDefault();
        //     //findedBolum..Add(new{ AppUserFavoritesId=findedBolum.Id,AppUserId=findedUser.Id });

        //     //dbcontext.SaveChanges();
        //     //dbcontext.ExcelData.Update(findedBolum);
        //     var result = dbcontext.AppUsers.Include(i => i.AppUserFavorites).ToList();

        //     return Ok(findedUser);

        // }
        [Authorize]
        [HttpGet]
        [Route("GetUserFavorites")]
        public async Task<IActionResult> GetUserFavorites(string username)
        {

            var result = await _appUserService.GetFavoritesByUserName(username);
            //var result = await _appUserService.GetAll();
            //Functions functions = new Functions();
            //var activeUser = _appUserService.FindByUserName("test@test.com");
            // var kadirUser = await _appUserService.FindByUserName("kadir");
            // var result = await _appUserService.FindByUserName(username);
            // TercihSihirbaziContext dbcontext = new TercihSihirbaziContext();

            // ProfileFavoriteDto favoriteDto = new ProfileFavoriteDto()
            // {
            //     AppUserId = 1,
            //     DetailObjectId = id
            // };
            //var result = await dbcontext.AppUserFavorites.Where(i => i.AppUserId == 1).ToListAsync();
            //var result = dbcontext.AppUserFavorites.ToList();
            return Ok(result);

        }
        [Authorize]
        [HttpGet]
        [Route("GetAppUserWithFavorites")]
        public async Task<IActionResult> GetAppUserWithFavorites()
        {
            var username = User.Claims.SingleOrDefault(i => i.Type == System.Security.Claims.ClaimTypes.Name).Value;
            var user = await _appUserService.GetAppUserWithFavorites(username);
            return Ok(user);
        }



        [HttpGet]
        [Route("AddFavoriteAsMember")]
        public async Task<IActionResult> AddFavoriteAsMember(int id)
        {
            Functions functions = new Functions();
            var user = await _appUserService.FindByUserName(User.Claims.SingleOrDefault(i => i.Type == System.Security.Claims.ClaimTypes.Name).Value);
            // ui halledildiğinde alltan devam edilecek username parametresi kaldırılacak..
            //var user = await _appUserService.FindByUserName(User.Identity.Name);
            var dbdata = await _excelDataService.GetAll();
            var findedId = dbdata.Where(i => i.ProgramKodu == id).Select(i => i.Id).FirstOrDefault();
            TercihSihirbaziContext dbcontext = new TercihSihirbaziContext();

            ProfileFavoriteDto AddedfavoriteDto = new ProfileFavoriteDto()
            {
                AppUserId = user.Id,
                DetailObjectId = findedId
            };

            await dbcontext.AppUserFavorites.AddAsync(_mapper.Map<AppUserFavorites>(AddedfavoriteDto));
            await dbcontext.SaveChangesAsync();
            var result = await dbcontext.AppUserFavorites.Where(i => i.AppUserId == user.Id).Select(i => i.DetailObjectId).ToListAsync();
            return Ok(result);

        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteFavoriteAsMember")]
        public async Task<IActionResult> DeleteFavoriteAsMember(int id)
        {
            Functions functions = new Functions();
            var user = await _appUserService.FindByUserName(User.Claims.SingleOrDefault(i => i.Type == System.Security.Claims.ClaimTypes.Name).Value);
            // ui halledildiğinde alltan devam edilecek username parametresi kaldırılacak..
            //var user = await _appUserService.FindByUserName(User.Identity.Name);
            var exceldb = await _excelDataService.GetAll();
            var findedId = exceldb.Where(i => i.ProgramKodu == id).Select(i => i.Id).FirstOrDefault();
            TercihSihirbaziContext dbcontext = new TercihSihirbaziContext();

            ProfileFavoriteDto AddedfavoriteDto = new ProfileFavoriteDto()
            {
                AppUserId = user.Id,
                DetailObjectId = id
            };
            var result = await dbcontext.AppUserFavorites.Where(i => i.AppUserId == user.Id && i.DetailObjectId == findedId).FirstOrDefaultAsync();
            dbcontext.AppUserFavorites.Remove(result);
            dbcontext.SaveChanges();
            //var result = await dbcontext.AppUserFavorites.Where(i => i.AppUserId == user.Id).Select(i => i.DetailObjectId).ToListAsync();
            return Ok(result.DetailObjectId);

        }

        [HttpGet]
        [Route("MostPreferredSectionForUsers")]
        public async Task<IActionResult> MostPreferredSectionForUsers()
        {
            using var context = new TercihSihirbaziContext();

            //var list = await context.AppUserFavorites.Select(i => i.DetailObjectId).ToListAsync();
            var list = await context.AppUserFavorites.Include("Favorited").Select(i => i.Favorited.ProgramAdi).ToListAsync();
            var q = list.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);

            return Ok(q);
        }

        [HttpGet]
        [Route("NumberOfSections")]
        public async Task<IActionResult> NumberOfSections()
        {
            using var context = new TercihSihirbaziContext();

            var list = await context.ExcelData.Select(i => i.ProgramAdi).ToListAsync();
            var q = list.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);

            return Ok(q);
        }

        [HttpGet]
        [Route("MostPreferredSection")]
        public async Task<IActionResult> MostPreferredSection(string? filter = null)
        {
            using var context = new TercihSihirbaziContext();
            List<DetailObject> list = new();
            if (filter != null)
            {
                if (filter == "SAY" || filter == "SÖZ" || filter == "EA" || filter == "DİL")
                {
                    list = await context.ExcelData.Where(i => i.PuanTuru == filter).ToListAsync();
                }
                else
                {
                    list = await context.ExcelData.ToListAsync();

                }
            }
            else
            {
                list = await context.ExcelData.ToListAsync();

            }


            var q = list.GroupBy(x => x.ProgramAdi)
            .Select(g => new { Value = g.Key, Count = g.Sum(x => x.Yerlesen) })
            .OrderByDescending(x => x.Count);

            return Ok(q);
        }


        [HttpPut]
        [Authorize(Roles = RoleInfo.Admin)]
        [ValidModel]
        public async Task<IActionResult> Update(ProfileUpdateDto profileUpdateDto)
        {
            await _profileService.Update(_mapper.Map<Entities.Concrete.Profile>(profileUpdateDto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleInfo.Admin)]
        [ServiceFilter(typeof(ValidId<Entities.Concrete.Profile>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _profileService.Remove(new Entities.Concrete.Profile() { Id = id });
            return NoContent();
        }

        [HttpGet("test/{id}")]
        [ServiceFilter(typeof(ValidId<AppUser>))]
        public IActionResult Test(int id)
        {
            return Ok();
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //loglama


            return Problem(detail: "api da bir hata olustu, en kisa zamanda düzeltilecek");
        }
    }
}