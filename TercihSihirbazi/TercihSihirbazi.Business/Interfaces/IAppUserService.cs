using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Dtos.AppUserDtos;
using TercihSihirbazi.Entities.Dtos.ProfileDtos;

namespace TercihSihirbazi.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> FindByUserName(string userName);
        Task<List<AppUserFavorites>> GetFavoritesByUserName(string userName);
        Task<ProfileUserFavoritesDto> GetAppUserWithFavorites(string userName);


        Task<bool> CheckPassword(AppUserLoginDto appUserLoginDto);
        Task<List<AppRole>> GetRolesByUserName(string userName);
    }
}
