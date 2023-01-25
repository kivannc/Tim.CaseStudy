﻿using LibraryAPI.DbContext;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
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

        public async Task<Member> GetUserByIdAsync(int id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
            return member;

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
            return await _context.SaveChangesAsync()>0;
            
        }
    }
}
