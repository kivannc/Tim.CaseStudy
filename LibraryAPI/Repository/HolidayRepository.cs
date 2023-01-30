using Library.API.DbContext;
using Library.API.Models;
using Library.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Repository;

public class HolidayRepository : IHolidayRepository
{
    private readonly LibraryDbContext _context;

    public HolidayRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Holiday>> GetHolidays()
    {
        return await _context.Holidays.ToListAsync();
    }
}