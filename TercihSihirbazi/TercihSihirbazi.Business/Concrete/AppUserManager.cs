using System.Collections.Generic;
using System.Threading.Tasks;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.DataAccess.Interfaces;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Dtos.AppUserDtos;

namespace TercihSihirbazi.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IAppUserDal _appUserDal;
        public AppUserManager(IGenericDal<AppUser> genericDal, IAppUserDal appUserDal) : base(genericDal)
        {
            _appUserDal = appUserDal;
        }

        public async Task<bool> CheckPassword(AppUserLoginDto appUserLoginDto)
        {
            var appUser = await _appUserDal.GetByFilter(I => I.UserName == appUserLoginDto.UserName);
            return appUser.Password == appUserLoginDto.Password ? true : false;
        }

        public async Task<AppUser> FindByUserName(string userName)
        {
            return await _appUserDal.GetByFilter(I => I.UserName == userName);
        }

        public async Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            return await _appUserDal.GetRolesByUserName(userName);
        }
    }
}
