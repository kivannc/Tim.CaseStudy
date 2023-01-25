using LibraryAPI.Models;

namespace LibraryAPI.Repository.Interface;

public interface IHolidayRepository
{
    Task<IEnumerable<Holiday>> GetHolidays();
}