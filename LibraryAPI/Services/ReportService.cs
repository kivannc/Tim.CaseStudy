using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;

namespace LibraryAPI.Services
{
    public class ReportService : IReportService
    {
        private const int UpcomingDays = 2;
        private const double PenaltyCoefficient = 0.20d;

        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

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
                transactionDto.Penalty = CalculatePenalty(transactionDto.LateDays);
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

        private double CalculatePenalty(int days)
        {
            var penalty = 0.0d;
            // First day penalty is 0
            for (int i = 1; i <= days; i++)
            {
                var prevPenalty = penalty;
                penalty = CalculateFibonacci(i) * PenaltyCoefficient + prevPenalty;
            }
            //return as 2 decimal places
            return Math.Round(penalty, 2);
        }
	
	    // Calculates the Fibonacci Number
        // Assumes Fibonacci starts with 0 
        private double CalculateFibonacci(int n )
        {
            if (n <= 1) return 0;
            var a = 0.0d;
            var b = 1.0d;
            for (var i = 2; i <= n-1; i++)
            {
                var c = a + b;
                a = b;
                b = c;
            }
            return b;
        }
    }
}
