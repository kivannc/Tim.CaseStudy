using LibraryAPI.Models;
using System.Linq.Expressions;
using System;

namespace LibraryAPI.Repository;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllUsersAsync();
    Task<Member> GetUserByIdAsync(int id);
    Task AddUserAsync(Member user);
    Task UpdateUserAsync(Member user);
    Task DeleteUserAsync(Member user);
    Task<bool> SaveChangesAsync();

}
