using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;

namespace LibraryAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        private const int UpcomingDays = 2;
        private const double PenaltyCost = 0.20d;
        public ReportService(
            ITransactionRepository transactionRepository,
            IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<ReportDto> GetDailyReportAsync()
        {
            //Get late transactions from database
            var lateTransactions =
                await _transactionRepository
                    .GetManyAsync(t => t.ReturnDate == null && t.DueDate < DateTime.Now);

            //Map them to DTOs
            var mappedLateTransactions = _mapper.Map<IEnumerable<TransactionDto>>(lateTransactions).ToArray();


            // Calculate penalty for each transaction. 
            // This could be done in the mapping, but I think this a business logic and implemented in here.
            // Penalty calculation logic may change later, so I think it is better to keep it here.
            
            foreach (var transactionDto in mappedLateTransactions)
            {
                if (transactionDto.BookStatus != BookStatus.Overdue) continue;
                transactionDto.Penalty = GetPenalty(transactionDto.DueDate);
            }

            //Get upcoming transactions from database

            //Get transactions that are due in the next 2 days
            var startDate = DateTime.Now.AddDays(-UpcomingDays);
            
            var upComingTransactions =
                await _transactionRepository.GetManyAsync(t => t.ReturnDate == null && t.DueDate >= startDate && t.DueDate <= DateTime.Now);


            // Map them to DTOs
            var mappedUpcomingTransactions = _mapper.Map<IEnumerable<TransactionDto>>(upComingTransactions);

            //Return the report
            return new ReportDto
            {
                LateTransactions = mappedLateTransactions,
                UpcomingTransactions = mappedUpcomingTransactions
            };
        }

        private double GetPenalty(DateTime dueDate)
        {

            var penalty = 0.0d;
            var lateDays = (DateTime.Now - dueDate).TotalDays;
            while (lateDays >= 0)
            {
                penalty += lateDays * PenaltyCost;
                lateDays--;
            }
            //return as 2 decimal places
            return Math.Round(penalty, 2);
        }
    }
}
