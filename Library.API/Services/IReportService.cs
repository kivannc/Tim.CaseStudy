using Library.API.Dtos;

namespace Library.API.Services;

public interface IReportService
{
    Task<ReportDto> GetDailyReportAsync();
}