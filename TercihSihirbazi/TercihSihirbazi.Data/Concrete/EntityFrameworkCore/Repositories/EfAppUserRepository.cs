using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Context;
using TercihSihirbazi.DataAccess.Interfaces;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Dtos.ProfileDtos;

namespace TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : EfGenericRepository<AppUser>, IAppUserDal
    {
        public async Task<ProfileUserFavoritesDto> GetAppUserWithFavorites(string userName)
        {
            // join kullanılması lazım
            using var context = new TercihSihirbaziContext();

            // var findedUser = await context.AppUsers.Where(i => i.UserName == userName).FirstOrDefaultAsync();
            // var user = await context.AppUsers.Where(i => i.UserName == userName).Include(i => i.AppUserFavorites.Where(i => i.FavoritedAppUsers == findedUser)).FirstOrDefaultAsync();
            //user.AppUserFavorites = await context.ExcelData.Where(i => i.FavoritedAppUsers == user).ToListAsync();
            //return user;
            var user = await context.AppUsers.Where(i => i.UserName == userName).FirstOrDefaultAsync();
            var favorites = await context.AppUserFavorites.Where(x => x.AppUserId == user.Id).Select(i => i.Favorited.Id).ToListAsync();
            var excelData = await context.ExcelData.Where(i => favorites.Contains(i.Id)).ToListAsync();
            //user.AppUserFavorites = favorites;

            ProfileUserFavoritesDto userFavorites = new ProfileUserFavoritesDto()
            {
                Username = user.UserName,
                Favorites = excelData
            };

            return userFavorites;

            // return await context.AppUsers.Join(context.AppUserFavorites, u => u.Id, uf => uf.AppUserId, (user, userFavorite) => new
            // {
            //     user = user,
            //     userFavorite = userFavorite
            // }).Join(context.ExcelData, two => two.userFavorite.DetailObjectId, ed => ed.Id, (twoTable, favorites) => new
            // {
            //     appUser = twoTable.user,
            //     appUserFavorites = twoTable.userFavorite,

            // })
            // .Where(i => i.appUser.UserName == userName).Select(i => new AppUser
            // {
            //     Id = i.appUser.Id,
            //     FullName = i.appUser.FullName,
            //     AppUserRoles = i.appUser.AppUserRoles,
            //     UserName = i.appUser.UserName,
            //     AppUserFavorites = i.appUser.AppUserFavorites
            // }).FirstOrDefaultAsync();
        }

        public async Task<List<AppUserFavorites>> GetFavoritesByUserName(string userName)
        {
            using var context = new TercihSihirbaziContext();
            var user = await context.AppUsers.FirstOrDefaultAsync(i => i.UserName == userName);
            return await context.AppUserFavorites.Where(i => i.AppUserId == user.Id).ToListAsync();
        }

        public async Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            using var context = new TercihSihirbaziContext();
            var result = await context.AppUsers.Join(context.AppUserRoles, u => u.Id, ur => ur.AppUserId, (user, userRole) => new
            {
                user = user,
                userRole = userRole
            }).Join(context.AppRoles, two => two.userRole.AppRoleId, r => r.Id, (twoTable, role) => new
            {
                user = twoTable.user,
                userRole = twoTable.userRole,
                role = role
            }).Where(I => I.user.UserName == userName).Select(I => new AppRole
            {
                Id = I.role.Id,
                Name = I.role.Name
            }).ToListAsync();
            return result;
        }
    }
}
