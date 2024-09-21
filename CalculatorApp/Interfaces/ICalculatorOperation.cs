using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Interfaces
{
    //This fallow ISP and OCP principles by having a single responsibility and being open for extension but closed for modification.
    public interface ICalculatorOperation
    {
        //add operator symbol used in the operation for adding to the formatted string
        string OperatorSymbol { get; }
        //This method calculates the result of the operation based on the numbers provided.
        double PerformOperation(List<int> numbers);
        //Add a method to return the formatted string of the formula, along with the calculation result.
        string GetFormattedString(List<int> numbers);


    }
}
