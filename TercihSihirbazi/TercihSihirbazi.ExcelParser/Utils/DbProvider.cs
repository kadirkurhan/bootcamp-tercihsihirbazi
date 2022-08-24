using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.Business.Concrete;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.Data.Concrete.EntityFrameworkCore.Repositories;
using TercihSihirbazi.Data.Interfaces;

namespace TercihSihirbazi.ExcelParser
{
    public static class Registration
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {
                //add your service registrations
                services.AddScoped<IExcelDataService, ExcelDataManager>();
                    services.AddScoped<IExcelDAL, EfExcelDataRepository>();

                });

            return hostBuilder;
        }
    } 

}
