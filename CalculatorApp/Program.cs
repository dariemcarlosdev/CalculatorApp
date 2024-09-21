using CalculatorApp.ConcreteOperations;
using CalculatorApp.Helpers;
using CalculatorApp.Interfaces;
using CalculatorApp.Services;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {

        // Create service collection and configure our services
        var serviceCollection = new ServiceCollection()
                 .AddSingleton<ICalculatorOperation, Addition>()
                .AddSingleton<Calculator>()
                .AddSingleton<ConsoleIO>()
                .BuildServiceProvider();

        // io service is used to get user input and write output
        var io = serviceCollection.GetService<ConsoleIO>();
        if (io == null)
        {
            Console.WriteLine("Failed to initialize ConsoleIO service.");
            return;
        }


        // calculator service is used to perform the operation
        var calculator = serviceCollection.GetService<Calculator>();
        //check if calculator service is null
        if (calculator == null)
        {
            Console.WriteLine("Failed to initialize Calculator service.");
            return;
        }

        while (true)
        {
            // Get user input
            var numbers = io.GetUserInput();
            //check if user input is null
            if (numbers == null)
            {
                io.PrintError("User input is null.");
                continue;
            }
            try

            {
                ////check if maximum number of input numbers is not exceeded of 2
                ///This cheching is not required as we are already checking in the PerformOperation method of the Calculator class, adherence to DRY principle and KISS principle.
                //if (GlobalHelperClass.ParseInput(numbers).Count > 2)
                //{
                //    //throw an exception if the maximum number of input numbers is exceeded of 2
                //    throw new Exception("Maximum number of input numbers is exceeded of 2");
                //}

                // Perform the operation and write the output
                var result = calculator.PerformOperation(numbers);
                io.WriteOutput(result);
            }
            catch (Exception ex)
            {
                // Print error message
                io.PrintError(ex.Message);
            }
        }
    }
}