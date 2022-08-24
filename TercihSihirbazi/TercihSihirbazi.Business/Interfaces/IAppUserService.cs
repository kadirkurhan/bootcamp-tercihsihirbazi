using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Dtos.AppUserDtos;

namespace TercihSihirbazi.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> FindByUserName(string userName);
        Task<bool> CheckPassword(AppUserLoginDto appUserLoginDto);
        Task<List<AppRole>> GetRolesByUserName(string userName);
    }
}
