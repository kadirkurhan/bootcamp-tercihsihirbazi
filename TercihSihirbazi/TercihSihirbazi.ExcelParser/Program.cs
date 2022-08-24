using System.Text.Json;
using System.Text;
using TercihSihirbazi.Entities.Concrete;
using IronXL;
using TercihSihirbazi.ExcelParser;
using TercihSihirbazi.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TercihSihirbazi.Data.Interfaces;
using System.Reflection;
using TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Context;

var host = Registration.CreateHostBuilder(args).Build();

//var _excelService = host.Services.GetService<IExcelDataService>();
//var _excelDal = host.Services.GetService<IExcelDAL>();

TercihSihirbaziContext dbContext = new();

JsonSerializerOptions jso = new JsonSerializerOptions();
jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
jso.WriteIndented = true;

//var path = System.AppDomain.CurrentDomain.BaseDirectory;

var enviroment = System.Environment.CurrentDirectory;
string binDirectory = Directory.GetParent(enviroment).Parent.FullName;
string projectDirectory = Directory.GetParent(binDirectory).Parent.FullName;

//Supported spreadsheet formats for reading include: XLSX, XLS, CSV and TSV
//WorkBook workbook = WorkBook.Load(@"D:\netCoreuygulamalarım\minimalapiExcelParser\netcoreapi\ExcelData\ExcelReader\Files\2020.xlsx");

for (int counter = 2020; counter < 2023; counter++)
{
    WorkBook workbook = WorkBook.Load($"{projectDirectory}\\TercihSihirbazi.ExcelParser\\Files\\{counter}.xlsx");

    WorkSheet sheet = workbook.WorkSheets.First();
    //int cellValue = sheet["A4"].IntValue;
    object myValue = sheet["A3"].StringValue;
    //Tuple<string, object> cellValuesDict = new Tuple<string, object>();
    List<DetailObject> cellValuesList = new List<DetailObject>();
    // Read from Ranges of cells elegantly.
    foreach (var cell in sheet["A3:K3"])
    {
        Console.WriteLine("Cell {0} has value '{1}'", cell.AddressString, cell.Text);
    }
    //foreach (var cell in sheet["A:K"])
    int i = 0;
    int sayac = 0;
    DetailObject detailObj = new DetailObject();

    YearOfExam yearOfExam = new YearOfExam();

    foreach (var cell in sheet["A:K"])
    {
        switch (i)
        {
            case 0:
                detailObj.ProgramKodu = Convert.ToInt32(cell.StringValue);
                i++;
                break;
            case 1:
                detailObj.ProgramAdi = cell.StringValue;
                i++;
                break;
            case 2:
                detailObj.PuanTuru = cell.StringValue;
                i++;
                break;
            case 3:
                try
                {
                    yearOfExam.GenelKontenjan = cell.IntValue;
                }
                catch
                {
                    //yearOfExam.GenelKontenjan = cell.StringValue;
                }
                i++;
                break;
            case 4:
                try
                {
                    yearOfExam.Yerlesen = cell.IntValue;
                }
                catch
                {
                    //yearOfExam.Yerlesen = cell.StringValue;
                }
                i++;
                break;
            case 5:
                try
                {
                    yearOfExam.EnKucukPuan = cell.IntValue;
                }
                catch
                {
                    // yearOfExam.EnKucukPuan = cell.StringValue;
                }
                i++;
                break;
            case 6:
                try
                {
                    yearOfExam.EnBuyukPuan = cell.IntValue;
                }
                catch
                {
                    // yearOfExam.EnBuyukPuan = cell.StringValue;
                }
                i++;
                break;
            case 7:
                try
                {
                    yearOfExam.OBKontenjan = cell.IntValue;
                }
                catch
                {
                    // yearOfExam.OBKontenjan = cell.StringValue;
                }
                i++;
                break;
            case 8:
                try
                {
                    yearOfExam.OBYerlesen = cell.IntValue;

                }
                catch
                {
                    // yearOfExam.OBYerlesen = cell.StringValue;
                }
                i++;
                break;
            case 9:
                try
                {
                    yearOfExam.OBKEnKucukPuan = cell.IntValue;

                }
                catch
                {
                    // yearOfExam.OBKEnKucukPuan = cell.StringValue;

                }
                i++;
                break;
            case 10:
                try
                {
                    yearOfExam.OBKEnBuyukPuan = cell.IntValue;
                }
                catch (Exception)
                {

                    // yearOfExam.OBKEnBuyukPuan = cell.StringValue;
                }
                if (counter==2020)
                {
                    detailObj.Year2020 = JsonSerializer.Serialize(yearOfExam, options: jso);
                }
                else if (counter == 2021)
                {
                    detailObj.Year2021 = JsonSerializer.Serialize(yearOfExam, options: jso);

                }
                else if (counter == 2022)
                {
                    detailObj.Year2022 = JsonSerializer.Serialize(yearOfExam, options: jso);

                }
                //cellValuesList.Add(detailObj);
                var exceldbdata = dbContext.ExcelData.ToList();
                

                var result = exceldbdata.Where(i => i.ProgramKodu == detailObj.ProgramKodu).FirstOrDefault();

                if (result!=null)
                {
                    if (counter == 2020)
                    {
                        if(result.Year2020 == null)
                        {
                            result.Year2020 = detailObj.Year2020;

                        }
                    }
                    else if (counter == 2021)
                    {
                        if (result.Year2021 == null)
                        {
                            result.Year2021 = detailObj.Year2021;
                        }

                    }
                    else if (counter == 2022)
                    {
                        if (result.Year2022 == null)
                        {
                            result.Year2022 = detailObj.Year2022;
                        }
                    }
                    dbContext.Update(result);
                    dbContext.SaveChanges();
                }
                else
                {
                    var mylist = dbContext.ExcelData.ToList();
                    dbContext.ExcelData.Add(detailObj);
                    dbContext.SaveChanges();
                }

                detailObj = new DetailObject();
                yearOfExam = new YearOfExam();

                //sayac++;
                //Console.WriteLine(sayac);
                i = 0;
                break;
            default:
                break;
        }
        //Console.WriteLine(i);
        //cellValuesDict.Add(sheet["A4"].StringValue, cell.Text);
        //cellValuesList.Add(JsonSerializer.Serialize(cell.Text));
    }


    //var myObj = JsonSerializer.Serialize(cellValuesList,options: jso);
    //var json = JsonSerializer.Serialize(detailObj, options: jso);

    //Console.WriteLine(myObj);
    //using (var client = new HttpClient())
    //{
    //    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

    //    var result = await client.PostAsync("http://localhost:39206/ExcelData/PostExcelData", data);
    //    string resultContent = await result.Content.ReadAsStringAsync();
    //    Console.WriteLine(resultContent);
    //}


    //foreach (var item in cellValuesList)
    //{
    //    Console.WriteLine(item);
    //}
}
Console.WriteLine("Migration Success!");
Console.ReadLine();




//OleDbConnection baglan;
//baglan = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\netCoreuygulamalarım\\Excel Parser\\ExcelParser\\ExcelParser\\Files\\tablo4_08072020.xlsx");

//baglan.Open();
//OleDbDataAdapter adapter = new OleDbDataAdapter("Select * from [Sheet1]", baglan);
//DataTable dt = new DataTable();
//adapter.Fill(dt);
//baglan.Close();