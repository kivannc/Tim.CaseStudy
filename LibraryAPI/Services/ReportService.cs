using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Repository.Interface;

namespace LibraryAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        private const int UpcomingDays = 2;
        public ReportService(
            ITransactionRepository transactionRepository, 
            IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<ReportDto> GetDailyReportAsync()
        {

            var lateTransactions =
                await _transactionRepository
                    .GetManyAsync(t => t.ReturnDate == null && t.DueDate < DateTime.Now);

            var mappedLateTransactions = _mapper.Map<IEnumerable<TransactionDto>>(lateTransactions);
            var startDate = DateTime.Now.AddDays(-UpcomingDays);
            var upComingTransactions =
                await _transactionRepository.GetManyAsync(t => t.ReturnDate == null &&  t.DueDate >= startDate && t.DueDate <= DateTime.Now);

            var mappedUpcomingTransactions = _mapper.Map<IEnumerable<TransactionDto>>(upComingTransactions);

            return new ReportDto
            {
                LateTransactions = mappedLateTransactions,
                UpcomingTransactions = mappedUpcomingTransactions
            };
        }
    }
}
