using Library.API.Models;

namespace Library.API.Repository.Interface;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllUsersAsync();
    Task<Member> GetMemberByIdAsync(int id);
    Task AddUserAsync(Member user);
    Task UpdateUserAsync(Member user);
    Task DeleteUserAsync(Member user);
    Task<bool> SaveChangesAsync();

}
