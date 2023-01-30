using Library.API.Dtos;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
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
