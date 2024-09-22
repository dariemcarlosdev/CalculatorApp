using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorApp.Interfaces;

namespace CalculatorApp.ConcreteOperations
{
    //Each class should have a single responsibility(SRP) and this class is responsible for addition operation. New operations can be added without modifying this class meet Open/Closed Principle.
    //According to Strategy Pattern, this concrete strategy class implements the ICalculatorOperationStrategy interface to perform the addition operation.
    public class AdditionStrategy : ICalculatorStrategy
    {
        public string OperatorSymbol => "+";

        public double PerformOperation(List<int> numbers) => numbers.Sum();

        public string GetFormattedString(List<int> numbers)
        {
            var validNumbers = numbers.Select(n => n.ToString());
            //return the formatted string of the formula, along with the calculation result.
            return string.Join(OperatorSymbol, validNumbers) + "=" + PerformOperation(numbers);
        }

    }
}
