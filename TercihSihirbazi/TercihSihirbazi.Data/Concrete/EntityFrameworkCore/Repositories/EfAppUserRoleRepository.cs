using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.DataAccess.Interfaces;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRoleRepository : EfGenericRepository<AppUserRole>, IAppUserRoleDal
    {
    }
}
