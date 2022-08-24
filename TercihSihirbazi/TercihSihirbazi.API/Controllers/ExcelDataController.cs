using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TercihSihirbazi.Business.Interfaces;

namespace TercihSihirbazi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelDataController : ControllerBase
    {
        private readonly IExcelDataService _excelDataService;

        public ExcelDataController(IExcelDataService excelDataService)
        {
            _excelDataService = excelDataService;
        }

        [HttpGet]
        public async Task<IActionResult> ExcelData(int page = 1, int recordPerPage=10) {
            var result = await _excelDataService.GetAll();
            
            return Ok(result.Skip(page).Take(recordPerPage));
        }
    }
}
