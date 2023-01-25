using LibraryAPI.DbContext;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository;

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