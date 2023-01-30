using Library.API.Dtos;
using Library.API.Models;

namespace Library.API.Profiles;

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

        CreateMap<BookSearchDto, Book>();
        CreateMap<BookTransaction, TransactionDto>();
    }

    

}