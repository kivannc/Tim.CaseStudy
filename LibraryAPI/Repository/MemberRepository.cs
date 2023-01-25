using LibraryAPI.Models;

namespace LibraryAPI.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public Task<IEnumerable<Member>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
