using CalculatorApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Services
{
    //This class is responsible for calculating the result of the operation, it uses the ICalculatorOperation interface to calculate the result and use DI to get the operation that can handle the input operation by depepending on abstraction (ICalculatorOperation) rather than concrete implementation.
    //I inject the operations that can handle the input operation using DI via contructor injection.
    //We can call this class as a Facade class as it hides the complexity of the operation and provides a simple interface to calculate the result.
    public class CalculatorService
    {
        private ICalculatorStrategy _operations;

        public CalculatorService(ICalculatorStrategy calculatorOperations)
        {
            _operations = calculatorOperations;
        }

        public void SetOperatioStrategy(ICalculatorStrategy calculatorOperations)
        {
            _operations = calculatorOperations;
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
                var result = _operations.GetFormattedString(numberList);
                return result;
            }
            
            catch (Exception)
            {

                throw;
            }





        }
    }
}
