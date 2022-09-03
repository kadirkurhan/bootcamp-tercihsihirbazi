using System.Text;
using TercihSihirbazi.Entities.Concrete;
using IronXL;
using TercihSihirbazi.ExcelParser;
using TercihSihirbazi.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TercihSihirbazi.Data.Interfaces;
using System.Reflection;
using TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Context;
using System.Text.Json;

IronXL.License.LicenseKey = "IRONXL.WDRLEOL.25866-CCD9A12761-HCUFEK25B6VFOHPA-DDTUDS6FXST7-Z3KBKUTF2JR2-MBWGQ2YVMASB-KDZII4ROFOJC-ZZTFSF-TFNVVZYTCE2HUA-DEPLOYMENT.TRIAL-D7ZEUD.TRIAL.EXPIRES.23.SEP.2022";

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

if (System.Diagnostics.Debugger.IsAttached)
{
    Console.WriteLine("debug modunda");
}

//Supported spreadsheet formats for reading include: XLSX, XLS, CSV and TSV
//WorkBook workbook = WorkBook.Load(@"D:\netCoreuygulamalarım\minimalapiExcelParser\netcoreapi\ExcelData\ExcelReader\Files\2020.xlsx");

for (int counter = 2021; counter < 2023; counter++)
{
    WorkBook workbook;
    try
    {
        workbook = WorkBook.Load($"{projectDirectory}\\TercihSihirbazi.ExcelParser\\Files\\{counter}.xlsx");

    }
    catch
    {
        var enviroment2 = System.Environment.CurrentDirectory;
        string binDirectory2 = Directory.GetParent(enviroment2).Parent.FullName;
        string projectDirectory2 = Directory.GetParent(binDirectory2).Parent.FullName;
        //workbook = WorkBook.Load($"{enviroment2}/TercihSihirbazi.ExcelParser/Files/{counter}.xlsx");
        workbook = WorkBook.Load($"{enviroment2}/TercihSihirbazi.ExcelParser/Files/{counter}.xlsx");
    }

    WorkSheet sheet = workbook.WorkSheets.First();
    //int cellValue = sheet["A4"].IntValue;
    //object myValue = sheet["A3"].StringValue;
    //Tuple<string, object> cellValuesDict = new Tuple<string, object>();
    List<DetailObject> cellValuesList = new List<DetailObject>();
    // Read from Ranges of cells elegantly.
    foreach (var cell in sheet["A3:N3"])
    {
        Console.WriteLine("Cell {0} has value '{1}'", cell.AddressString, cell.Text);
    }
    //foreach (var cell in sheet["A:K"])
    int i = 0;
    int sayac = 0;
    DetailObject detailObj = new DetailObject();

    YearOfExam yearOfExam = new YearOfExam();

    foreach (var cell in sheet["A:N"])
    {
        switch (i)
        {
            case 0:
                detailObj.ProgramKodu = Convert.ToInt32(cell.StringValue);
                i++;
                break;
            case 1:
                detailObj.UniversiteTuru = cell.StringValue;
                i++;
                break;
            case 2:
                detailObj.UniversiteAdi = cell.StringValue;
                i++;
                break;
            case 3:
                detailObj.FakulteAdi = cell.StringValue;
                i++;
                break;
            case 4:
                detailObj.ProgramAdi = cell.StringValue;
                i++;
                break;
            case 5:
                detailObj.PuanTuru = cell.StringValue;
                i++;
                break;
            case 6:

                yearOfExam.GenelKontenjan = Convert.ToInt32(cell.Text);

                i++;
                break;
            case 7:
                if (cell.Text == "--")
                {
                    yearOfExam.Yerlesen = 0;
                }
                else
                {
                    yearOfExam.Yerlesen = Convert.ToInt32(cell.Text);
                    detailObj.Yerlesen = yearOfExam.Yerlesen;
                }
                i++;
                break;
            case 8:
                if (cell.Text == "--")
                {
                    yearOfExam.EnKucukPuan = 0;
                }
                else
                {
                    yearOfExam.EnKucukPuan = Convert.ToInt32(Convert.ToDouble(cell.Text));
                }
                i++;
                break;
            case 9:
                if (cell.Text == "--")
                {
                    yearOfExam.EnBuyukPuan = 0;
                }
                else
                {
                    yearOfExam.EnBuyukPuan = Convert.ToInt32(Convert.ToDouble(cell.Text));
                }
                i++;
                break;
            case 10:
                if (cell.Text == "--")
                {
                    yearOfExam.OBKontenjan = 0;
                }
                else
                {
                    yearOfExam.OBKontenjan = Convert.ToInt32(cell.Text);
                }
                i++;
                break;
            case 11:
                if (cell.Text == "--")
                {
                    yearOfExam.OBYerlesen = 0;
                }
                else
                {
                    yearOfExam.OBYerlesen = Convert.ToInt32(cell.Text);
                }
                i++;
                break;
            case 12:
                if (cell.Text == "--")
                {
                    yearOfExam.OBKEnKucukPuan = 0;
                }
                else
                {
                    yearOfExam.OBKEnKucukPuan = Convert.ToInt32(Convert.ToDouble(cell.Text));
                }
                i++;
                break;
            case 13:
                if (cell.Text == "--")
                {
                    yearOfExam.OBKEnBuyukPuan = 0;
                }
                else
                {
                    yearOfExam.OBKEnBuyukPuan = Convert.ToInt32(Convert.ToDouble(cell.Text));
                }
                if (counter == 2020)
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

                if (result != null)
                {
                    if (counter == 2020)
                    {
                        if (result.Year2020 == null)
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