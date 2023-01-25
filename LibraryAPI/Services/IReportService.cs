using LibraryAPI.Dtos;

namespace LibraryAPI.Services;

public interface IReportService
{
    Task<ReportDto> GetDailyReportAsync();
}