using PersonalOnlineCalculator.Models;
using PersonalOnlineCalculator.Data;

namespace PersonalOnlineCalculator.Services
{
    public class CalculationService
    {
        public Calculation PerformCalculation(string expression)
        {
            // Dummy logic for calculation - replace with real implementation.
            var result = "42"; // Use an actual expression evaluator here.

            return new Calculation
            {
                Expression = expression,
                Result = result
            };
        }

        public void SaveCalculation(int userId, Calculation calculation)
        {
            Database.AddCalculation(userId, calculation.Expression, calculation.Result);
        }

        public List<Calculation> GetCalculationsForUser(int userId)
        {
            return Database.GetCalculationsForUser(userId);
        }

        public void DeleteCalculation(int calculationId)
        {
            Database.DeleteCalculation(calculationId);
        }
    }
}
