using System.Runtime.InteropServices.ComTypes;
using LibraryAPI.Dtos;
using LibraryAPI.Models;

namespace LibraryAPI.Profiles;

public class LibraryProfile : AutoMapper.Profile
{
    private const double PenaltyCost = 0.20d;

    public LibraryProfile()
    {
        // Source => Target
        CreateMap<Member, MemberDto>();
        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.BookStatus,
                opt => opt.MapFrom(src => GetBookStatus(src.BookTransactions)));

        CreateMap<BookTransaction, TransactionDto>()
            .ForMember(
                dest => dest.Penalty,
                opt => opt.MapFrom(src => GetPenalty(src)));

    }

    private double GetPenalty(BookTransaction transaction)
    {
        if (transaction.BookStatus != BookStatus.Overdue)
        {
            return 0;
        }

        var penalty = 0.0d;
        var lateDays = (DateTime.Now - transaction.DueDate).TotalDays;
        while (lateDays >= 0)
        {
            penalty += lateDays * PenaltyCost;
            lateDays--;
        }

        return penalty;
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