using System.Collections.Generic;
using System.Threading.Tasks;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.DataAccess.Interfaces
{
    public interface IAppUserDal : IGenericDal<AppUser>
    {
        Task<List<AppRole>> GetRolesByUserName(string userName);
    }
}
