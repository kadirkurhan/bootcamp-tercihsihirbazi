using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.Data.Interfaces;
using TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.Data.Concrete.EntityFrameworkCore.Repositories
{
    public class EfExcelDataRepository:EfGenericRepository<DetailObject>, IExcelDAL
    {
    }
}
