using CalculatorApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Services
{
    //This class is responsible for managing the calculator context, it can be called as a Facade class as it hides the complexity of the context management and provides a simple interface to manage the context.
    //Provides a simple interface to manage the context, which is going to be used by the CalculatorService class to perform the operation.
    internal class CalculatorContext
    {
        //The Context has a reference to one of the Strategy objects and delegates the operation to the strategy object.
        //The Context is responsible for managing the strategy object that is going to be used by the client.
        //The Context does not know the concrete class of a Strategy and how to perform the operation, it delegates the operation to the strategy object.
        //It should should work with all strategies object via the Strategy interface and be able to switch the strategy object at runtime.
        private ICalculatorStrategy _calculatorStrategy;

        public CalculatorContext(ICalculatorStrategy calculatorOperations)
        {
            _calculatorStrategy = calculatorOperations;
        }

        // The Client will set what calculation strategy to use by calling this method at the runtime.
        public void SetOperatioStrategy(ICalculatorStrategy calculatorOperations)
        {
            _calculatorStrategy = calculatorOperations;
        }

        public string PerformOperation(string numbers)
        {
            //Parse the input string to get the numbers
            List<int> numberList = Helpers.GlobalHelperClass.ParseInput(numbers);

            try
                {
                    //throw an exception if the number list is empty or exceeds the limit of 2 numbers as max 2 numbers are allowed for the operation.
                    //Removing this check will allow the operation to handle more than 2 numbers.
                    //if (numberList.Count == 0 || numberList.Count > 2)
                    //{
                    //    throw new Exception("Invalid input, please enter max 2 numbers separated by commas");
                    //}

                    //Calculate the result of the operation
                    var result = _calculatorStrategy.GetFormattedString(numberList);
                    return result;
                }

                catch (Exception)
                {

                    throw;
                }
        }
    }
}
