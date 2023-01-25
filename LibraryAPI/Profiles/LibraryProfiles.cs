using System.Runtime.InteropServices.ComTypes;
using LibraryAPI.Dtos;
using LibraryAPI.Models;

namespace LibraryAPI.Profiles;

public class LibraryProfile : AutoMapper.Profile {


    public LibraryProfile()
    {
        // Source => Target
        CreateMap<Member, MemberDto>();
        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.BookStatus,
                opt => opt.MapFrom(src => src.BookTransactions == null || src.BookTransactions.Count == 0
                    ? BookStatus.Available
                    : src.BookTransactions.First().BookStatus));
        
        CreateMap<BookTransaction, TransactionDto>();
    }

    

}