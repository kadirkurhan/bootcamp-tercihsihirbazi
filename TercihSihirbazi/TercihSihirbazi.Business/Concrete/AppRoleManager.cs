using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.DataAccess.Interfaces;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.Business.Concrete
{
    public class AppRoleManager : GenericManager<AppRole>,IAppRoleService
    {
        private readonly IGenericDal<AppRole> _genericDal;
        public AppRoleManager(IGenericDal<AppRole> genericDal): base(genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<AppRole> FindByName(string roleName)
        {
            return await _genericDal.GetByFilter(I => I.Name == roleName);
        }
    }
}
