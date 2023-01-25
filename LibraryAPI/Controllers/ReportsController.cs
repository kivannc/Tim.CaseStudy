using LibraryAPI.Dtos;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet]
        public async Task<ActionResult<ReportDto>> Get()
        {
            var reports = await _reportService.GetDailyReportAsync();
            return Ok(reports);
        }
    }
}
