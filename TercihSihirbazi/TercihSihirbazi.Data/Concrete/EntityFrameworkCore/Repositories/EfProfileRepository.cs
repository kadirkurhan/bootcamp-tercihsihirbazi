using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Context;
using TercihSihirbazi.DataAccess.Interfaces;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfProfileRepository : EfGenericRepository<Profile>, IProfileDal
    {
       
    }
}
