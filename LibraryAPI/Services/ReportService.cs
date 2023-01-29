using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;

namespace LibraryAPI.Services;

public class ReportService : IReportService
{
    private const int UpcomingDays = 2;
    private readonly IMapper _mapper;

    private readonly ITransactionRepository _transactionRepository;
    private readonly IPenaltyService _penaltyService;

    public ReportService(
        ITransactionRepository transactionRepository,
        IMapper mapper, IPenaltyService penaltyService)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _penaltyService = penaltyService;
    }

    public async Task<ReportDto> GetDailyReportAsync()
    {
        //Get late transactions from database
        var lateTransactions =
            await _transactionRepository
                .GetManyAsync(t => t.ReturnDate == null && t.DueDate < DateTime.Today);

        //Map them to DTOs
        var mappedLateTransactions = _mapper.Map<IEnumerable<TransactionDto>>(lateTransactions).ToArray();


        // Calculate penalty for each transaction. 
        // This could be done in the mapping, but I think this a business logic and implemented in here.
        // Penalty calculation logic may change later, so I think it is better to keep it here.

        foreach (var transactionDto in mappedLateTransactions)
        {
            if (transactionDto.BookStatus != BookStatus.Overdue) continue;
            transactionDto.Penalty = _penaltyService.CalculatePenalty(transactionDto.LateDays);
        }

        //Get upcoming transactions from database
        //Get transactions that are due in the last 2 days
        var startDate = DateTime.Today.AddDays(-UpcomingDays);

        var upComingTransactions =
            await _transactionRepository.GetManyAsync(t =>
                t.ReturnDate == null && t.DueDate >= startDate && t.DueDate > DateTime.Today);


        // Map them to DTOs
        var mappedUpcomingTransactions = _mapper.Map<IEnumerable<TransactionDto>>(upComingTransactions);

        //Return the report
        return new ReportDto
        {
            LateTransactions = mappedLateTransactions,
            UpcomingTransactions = mappedUpcomingTransactions
        };
    }


}