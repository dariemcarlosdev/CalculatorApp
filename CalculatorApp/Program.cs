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
                 //registering concrete operation classes in the service collection
                 .AddSingleton<AdditionStrategy>() 
                 .AddSingleton<SubtractionStrategy>()
                 .AddSingleton<MultiplicationStrategy>()
                 .AddSingleton<DivisionStrategy>()
                 //registering ICalculatorStrategy interface and its corresponding implementation
                 .AddSingleton<ICalculatorStrategy, AdditionStrategy>()
                 .AddSingleton<ICalculatorStrategy, SubtractionStrategy>()
                 .AddSingleton<ICalculatorStrategy, MultiplicationStrategy>()
                 .AddSingleton<ICalculatorStrategy, DivisionStrategy>()
                 //To handle multiple strategies dynamically without hardcoding them in a switch statement by entering an operation, I use a dictionary to map operators to their corresponding strategies.This approach allows me to register and resolve strategies dynamically.
                 //and handle dependency injection for multiple implementations of the same interface dynamically.
                 .AddSingleton(provider => new Dictionary<string, ICalculatorStrategy>
                {
                    { "+", provider.GetService<AdditionStrategy>() },
                    { "-", provider.GetService<SubtractionStrategy>() },
                    { "*", provider.GetService<MultiplicationStrategy>() },
                    { "/", provider.GetService<DivisionStrategy>() }
                     //Add other operations here by adding the key value pair of the operation symbol and the corresponding operation class

                 })
                //registering the CalculatorContext class in the service collection to manage the calculator context. it depends on ICalculatorStrategy interface, which has multiple implementations, That why I registered the ICalculatorStrategy interface and its corresponding implementation in the service collection.
                .AddSingleton<CalculatorContext>()
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
        var calculatorContext = serviceCollection.GetService<CalculatorContext>(); //Issue solved: here was a dependency injection error since the CalculatorContext class depends on ICalculatorStrategy interface, which has multiple implementations, so I need to register the ICalculatorStrategy interface and its corresponding implementation in the service collection.

        //check if calculator service has been initialized successfully
        if (calculatorContext == null)
        {
            Console.WriteLine("Failed to initialize Calculator service.");
            return;
        }

        while (true)
        {
            // Get user input
            var (numbers, operators) = io.GetUserInput();
            
            //check if user input is null
            if (numbers == null)
            {
                io.PrintError("User input is null.");
                continue;
            }
            else if (operators == string.Empty)
            {
                io.PrintError("You must to select a valid operator.");
                continue;
            }
            try

            {
                // dynamic resolution using the dictionary of strategies
                var strategies = serviceCollection.GetService<Dictionary<string, ICalculatorStrategy>>();
                // Check if the operator is supported
                if (strategies.TryGetValue(operators, out var strategy))
                {
                    calculatorContext.SetOperatioStrategy(strategy);
                }
                else
                {
                    io.PrintError("Unknown operator.Pls,Enter a supported operator.");
                    continue;
                }

                // Perform the operation and write the output
                var result = calculatorContext.PerformOperation(numbers);
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