using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelDataController : ControllerBase
    {
        private readonly IExcelDataService _excelDataService;
        private readonly IAppUserService _appUserService;

        public ExcelDataController(IExcelDataService excelDataService, IAppUserService appUserService)
        {
            _excelDataService = excelDataService;
            _appUserService = appUserService;
        }

        [HttpGet]
        public async Task<IActionResult> ExcelData(int page = 1, int recordPerPage = 3)
        {
            List<DetailObject> result = await _excelDataService.GetAll();
            result = await _excelDataService.KontenjanMapping(result.Skip(page).Take(recordPerPage).ToList());
            return Ok(result);
        }

        // [HttpPost]
        // [Route("AddFavorites")]
        // public async Task<IActionResult> AddFavorites(int id)
        // {
        //     List<DetailObject> result = await _appUserService.acti
        //     result = await _excelDataService.KontenjanMapping(result.Skip(page).Take(recordPerPage).ToList());
        //     return Ok(result);
        // }

    }
}
