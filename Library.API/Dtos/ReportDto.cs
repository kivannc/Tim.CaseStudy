namespace Library.API.Dtos;

public class ReportDto
{
    public IEnumerable<TransactionDto> LateTransactions { get; set; }
    public IEnumerable<TransactionDto> UpcomingTransactions { get; set; }
}