namespace LibraryAPI.Services;

public class PenaltyService : IPenaltyService
{
    private const double PenaltyCoefficient = 0.20d;

    public double CalculatePenalty(int days)
    {
        var penalty = 0.0d;
        // First day penalty is 0
        for (var i = 1; i <= days; i++)
        {
            var prevPenalty = penalty;
            penalty = CalculateFibonacci(i) * PenaltyCoefficient + prevPenalty;
        }

        //return as 2 decimal places
        return Math.Round(penalty, 2);
    }

    // Calculates the Fibonacci Number
    // Assumes Fibonacci starts with 0 from the example calculation 
    private double CalculateFibonacci(int n)
    {
        if (n <= 1) return 0;
        var a = 0.0d;
        var b = 1.0d;
        for (var i = 2; i <= n - 1; i++)
        {
            var c = a + b;
            a = b;
            b = c;
        }

        return b;
    }
}