using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.DataAccess.Interfaces;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Business.Dtos;

namespace TercihSihirbazi.Business.Concrete
{
    public class ExcelDataManager : GenericManager<DetailObject>, IExcelDataService
    {
        public ExcelDataManager(IGenericDal<DetailObject> genericDal) : base(genericDal)
        {
        }

        public async Task<List<DetailObject>> KontenjanMapping(List<DetailObject> list)
        {
            YearOfExamDto sonuc;
            var Kontenjan = list.Select(i => i.Year2021).ToList();
            Dictionary<string, List<int>> dict = new();
            foreach (DetailObject item in list)
            {
                YearOfExamListDto objectList = new();
                List<int> genericList = new();
                List<int> YerlesenList = new();
                List<int> TavanList = new();
                List<int> TabanList = new();

                //objectList.genericList.Add(item);
                if (item.Year2021 != null)
                {
                    sonuc = JsonSerializer.Deserialize<YearOfExamDto>(item.Year2021);
                    genericList.Add(sonuc.GenelKontenjan);
                    if (!dict.ContainsKey("kontenjan"))
                    {
                        dict.Add("kontenjan", genericList);
                    }
                    else
                    {
                        dict["kontenjan"] = genericList;
                    }
                }
                if (item.Year2022 != null)
                {
                    sonuc = JsonSerializer.Deserialize<YearOfExamDto>(item.Year2022);
                    genericList.Add(sonuc.GenelKontenjan);
                    if (!dict.ContainsKey("kontenjan"))
                    {
                        dict.Add("kontenjan", genericList);
                    }
                    else
                    {
                        dict["kontenjan"] = genericList;
                    }
                }
                // Yerleşen


                if (item.Year2021 != null)
                {
                    sonuc = JsonSerializer.Deserialize<YearOfExamDto>(item.Year2021);
                    YerlesenList.Add(sonuc.Yerlesen);
                    if (!dict.ContainsKey("yerlesen"))
                    {
                        dict.Add("yerlesen", YerlesenList);
                    }
                    else
                    {
                        dict["yerlesen"] = YerlesenList;
                    }
                }
                if (item.Year2022 != null)
                {
                    sonuc = JsonSerializer.Deserialize<YearOfExamDto>(item.Year2022);
                    YerlesenList.Add(sonuc.Yerlesen);
                    if (!dict.ContainsKey("yerlesen"))
                    {
                        dict.Add("yerlesen", YerlesenList);
                    }
                    else
                    {
                        dict["yerlesen"] = YerlesenList;
                    }
                }

                // Taban
                if (item.Year2021 != null)
                {
                    sonuc = JsonSerializer.Deserialize<YearOfExamDto>(item.Year2021);
                    TabanList.Add(sonuc.EnKucukPuan);
                    if (!dict.ContainsKey("EnKucukPuan"))
                    {
                        dict.Add("EnKucukPuan", TabanList);
                    }
                    else
                    {
                        dict["EnKucukPuan"] = TabanList;
                    }
                }
                if (item.Year2022 != null)
                {
                    sonuc = JsonSerializer.Deserialize<YearOfExamDto>(item.Year2022);
                    TabanList.Add(sonuc.EnKucukPuan);
                    if (!dict.ContainsKey("EnKucukPuan"))
                    {
                        dict.Add("EnKucukPuan", TabanList);
                    }
                    else
                    {
                        dict["EnKucukPuan"] = TabanList;
                    }
                }

                // Tavan
                if (item.Year2021 != null)
                {
                    sonuc = JsonSerializer.Deserialize<YearOfExamDto>(item.Year2021);
                    TavanList.Add(sonuc.EnBuyukPuan);
                    if (!dict.ContainsKey("EnBuyukPuan"))
                    {
                        dict.Add("EnBuyukPuan", TavanList);
                    }
                    else
                    {
                        dict["EnBuyukPuan"] = TavanList;
                    }
                }
                if (item.Year2022 != null)
                {
                    sonuc = JsonSerializer.Deserialize<YearOfExamDto>(item.Year2022);
                    TavanList.Add(sonuc.EnBuyukPuan);
                    if (!dict.ContainsKey("EnBuyukPuan"))
                    {
                        dict.Add("EnBuyukPuan", TavanList);
                    }
                    else
                    {
                        dict["EnBuyukPuan"] = TavanList;
                    }
                }
                //objectList.GenelKontenjan.AddRange(genericList);

                //List<int> genelKontenjan = objectList.GenelKontenjan;
                //List<int> genelKontenjan = genericList;
                objectList.GenelKontenjan = genericList;
                objectList.Yerlesen = YerlesenList;
                item.TabanList = JsonSerializer.Serialize(TabanList);
                item.TabanList.Substring(1, item.TabanList.Length - 2);
                item.TavanList = JsonSerializer.Serialize(TavanList);
                item.TavanList.Substring(1, item.TavanList.Length - 2);
                Console.WriteLine(item.TavanList);
                item.Year2018 = JsonSerializer.Serialize(objectList);
            }
            //var deserialized = JsonSerializer.Deserialize<DetailObject>(Kontenjan);
            return list;
        }

    }
}
