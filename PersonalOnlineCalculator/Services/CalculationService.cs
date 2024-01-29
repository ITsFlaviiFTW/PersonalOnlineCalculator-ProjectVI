using System;
using System.Collections.Generic;
using System.Data; // Include this for DataTable.Compute
using PersonalOnlineCalculator.Models;
using PersonalOnlineCalculator.Data;

namespace PersonalOnlineCalculator.Services
{
    public class CalculationService
    {
        public Calculation PerformCalculation(string expression)
        {
            try
            {
                // Use DataTable Compute to evaluate the expression
                var result = new DataTable().Compute(expression, null).ToString();
                return new Calculation
                {
                    Expression = expression,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex.Message) and handle it appropriately
                throw new InvalidOperationException("An error occurred during calculation.", ex);
            }
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