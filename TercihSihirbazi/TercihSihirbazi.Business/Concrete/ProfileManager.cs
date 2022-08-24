using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.DataAccess.Interfaces;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.Business.Concrete
{
    public class ProfileManager : GenericManager<Profile>, IProfileService
    {
        private readonly IProfileDal _profileDal;
        public ProfileManager(IGenericDal<Profile> genericDal,
            IProfileDal profileDal) : base(genericDal)
        {
            _profileDal = profileDal;
        }

       

    }
}
