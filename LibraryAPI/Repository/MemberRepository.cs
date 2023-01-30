using Library.API.DbContext;
using Library.API.Models;
using Library.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryDbContext _context;

    public MemberRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Member>> GetAllUsersAsync()
    {
        return await _context.Members.ToListAsync();
    }

    public async Task<Member> GetMemberByIdAsync(int id)
    {
        return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
    }

    public Task AddUserAsync(Member user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(Member user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(Member user)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}