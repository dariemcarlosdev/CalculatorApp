using CalculatorApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.ConcreteOperations
{
    //Each class should have a single responsibility(SRP) and this class is responsible for subtraction operation. New operations can be added without modifying this class meet Open/Closed Principle.
    //According to Strategy Pattern, this concrete strategy class implements the ICalculatorOperationStrategy interface to perform the subtraction operation.
    internal class SubtractionStrategy : ICalculatorStrategy
    {
        public string OperatorSymbol => "-";

        public string GetFormattedString(List<int> numbers)
        {
            var validNumbers = numbers.Select(n => n.ToString());
            //return the formatted string of the formula, along with the calculation result.
            return string.Join(OperatorSymbol, validNumbers) + "=" + PerformOperation(numbers);
        }

        public double PerformOperation(List<int> numbers)
        {
            if (numbers.Count == 0)
            {
                return 0;
            }

            //The subtraction operation all elements in the list
            double result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                result -= numbers[i];
            }
            return result;

        }

    }
}
