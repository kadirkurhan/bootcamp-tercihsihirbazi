using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.DataAccess.Interfaces;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.Business.Concrete
{
    public class ExcelDataManager : GenericManager<DetailObject>, IExcelDataService
    {
        public ExcelDataManager(IGenericDal<DetailObject> genericDal) : base(genericDal)
        {
        }
    }
}
