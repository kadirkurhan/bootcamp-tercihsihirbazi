using System.Collections.Generic;
using System.Threading.Tasks;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Dtos.ProfileDtos;

namespace TercihSihirbazi.DataAccess.Interfaces
{
    public interface IAppUserDal : IGenericDal<AppUser>
    {
        Task<List<AppRole>> GetRolesByUserName(string userName);
        Task<List<AppUserFavorites>> GetFavoritesByUserName(string userName);
        public Task<ProfileUserFavoritesDto> GetAppUserWithFavorites(string userName);


    }
}
