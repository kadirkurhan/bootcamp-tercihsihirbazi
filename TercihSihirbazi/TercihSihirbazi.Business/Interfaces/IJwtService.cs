using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.Business.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwt(AppUser appUser, List<AppRole> roles);
    }
}
