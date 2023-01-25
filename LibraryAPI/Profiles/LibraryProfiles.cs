using LibraryAPI.Dtos;
using LibraryAPI.Models;

namespace LibraryAPI.Profiles;

public class LibraryProfile : AutoMapper.Profile
{
    public LibraryProfile()
    {
        // Source => Target
        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.BookStatus,
                opt => opt.MapFrom(src => GetBookStatus(src.BookTransactions)));
    }

    private BookStatus GetBookStatus(ICollection<BookTransaction> srcBookTransactions)
    {
        if (srcBookTransactions == null) return BookStatus.Available;
        if (srcBookTransactions.Count == 0)
        {
            return BookStatus.Available;
        }
        var transaction = srcBookTransactions.FirstOrDefault(t => t.ReturnDate == null);
        if (transaction == null) return BookStatus.Available;
        if (DateTime.Now > transaction.DueDate)
        {
            return BookStatus.Overdue;
        }

        return BookStatus.Borrowed;
    }
}