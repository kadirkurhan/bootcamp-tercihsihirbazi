using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.Entities.Interfaces;

namespace TercihSihirbazi.Entities.Concrete
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }
    public class DetailObject : BaseEntity, ITable
    {
        public int ProgramKodu { get; set; }
        public string ProgramAdi { get; set; }
        public string PuanTuru { get; set; }
        public string Year2018 { get; set; }
        public string Year2019 { get; set; }
        public string Year2020 { get; set; }
        public string Year2021 { get; set; }
        public string Year2022 { get; set; }
        public string Year2023 { get; set; }
        public string Year2024 { get; set; }
        public string Year2025 { get; set; }
        public string Year2026 { get; set; }
        public string Year2027 { get; set; }
        public string Year2028 { get; set; }

        public virtual ICollection<AppUser> FavoritedAppUsers { get; set; }
    }

    public class YearOfExam
    {
        public int GenelKontenjan { get; set; }
        public int Yerlesen { get; set; }
        public int EnKucukPuan { get; set; }
        public int EnBuyukPuan { get; set; }
        public int OBKontenjan { get; set; }
        public int OBYerlesen { get; set; }
        public int OBKEnKucukPuan { get; set; }
        public int OBKEnBuyukPuan { get; set; }
    }
    /*
    'ProgramKodu'
    'ProgramAdi'
    'PuanTuru'
    'GenelKontenjan'
    'Yerlesen'
    'EnKucukPuan'
    'EnBuyukPuan'
    'OBKontenjan'
    'OBYerlesen'
    'OBKEnKucukPuan'
    'OBKEnBuyukPuan' 
     */
}
