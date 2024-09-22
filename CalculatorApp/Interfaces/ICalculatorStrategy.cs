using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Interfaces
{
    //This fallow ISP and OCP principles by having a single responsibility and being open for extension but closed for modification.
    //This interface is responsible for defining the contract for the operation strategy, declares the methods and properties that are common to all the operations. The context class uses this interface to perform the operation by concrete operation classes.
    public interface ICalculatorStrategy
    {
        //add operator symbol used in the operation for adding to the formatted string
        string OperatorSymbol { get; }
        //This method calculates the result of the operation based on the numbers provided.
        double PerformOperation(List<int> numbers);
        //Add a method to return the formatted string of the formula, along with the calculation result.
        string GetFormattedString(List<int> numbers);


    }
}
