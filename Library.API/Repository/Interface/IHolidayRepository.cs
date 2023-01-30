using Library.API.Models;

namespace Library.API.Repository.Interface;

public interface IHolidayRepository
{
    Task<IEnumerable<Holiday>> GetHolidays();
}