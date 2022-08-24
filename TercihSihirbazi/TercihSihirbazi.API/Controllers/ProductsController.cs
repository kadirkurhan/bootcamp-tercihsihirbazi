using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.Business.StringInfos;
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
        private readonly IMapper _mapper;
        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            _mapper = mapper;
            _profileService = profileService;
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
        [Authorize(Roles=RoleInfo.Admin)]
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