using CalculatorApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Services
{
    //This class represent the console input and output operations, adherence to SRP principle, where each class should have a single responsibility.
    //It can be called as a Facade class as it hides the complexity of the input and output operations and provides a simple interface to read and write the input and output.
    public class ConsoleIO
    {
        //This method is responsible for reading the input from the console
        public string GetUserInput()
        {
            //Read the input from the console
            Console.Write("Enter numbers separated by commas: ");
            string input = Console.ReadLine();

            return input;


        }

        //This method is responsible for writing the output to the console
        public void WriteOutput(string output)
        {
            //Write the output to the console
            Console.WriteLine($"Result: {output}");
        }

        internal void PrintError(string message)
        {
            Console.WriteLine($"Error: {message}");
        }
    }
}
