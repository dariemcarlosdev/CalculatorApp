# CalculatorApp Overview

This repository contains the source code and documentation for the Calculator App, which has been refactored to adhere to SOLID programming principles, design patterns, and object-oriented programming (OOP) fundamentals. The goal of this project is to demonstrate how these principles and patterns can be applied to improve the maintainability, scalability, and readability of a software application.

## SOLID Design Principles and Design Patterns Applied in the Calculator App

### Single Responsibility Principle (SRP):

What it is: A class should have only one reason to change, meaning it should only have one job or responsibility.

How it's applied: Each operation (addition, subtraction, etc.) is encapsulated in its own class (e.g., Addition, Subtraction). This keeps each class focused on a single task, making the code more maintainable and easier to modify without affecting other parts of the system.

### Open/Closed Principle (OCP):

What it is: Software entities or components (classes, modules, functions) should be open for extension but closed for modification.

How it's applied: The calculator supports new operations by adding new classes that implement the ICalculatorStrategy interface, without modifying the existing CalculatorContext class. This makes the system extensible without needing to change existing code.

### Liskov Substitution Principle (LSP):

What it is: Objects of a superclass should be replaceable with objects of a subclass without affecting the correctness of the program.

How it's applied: Each operation class (Addition, Subtraction, etc.) implements the ICalculatorStrategy interface and can be substituted in the CalculatorContext class. Any operation can be swapped without breaking the CalculatorContext’s functionality.

### Interface Segregation Principle (ISP):

What it is: Clients should not be forced to depend on interfaces they do not use.

How it's applied: The ICalculatorStrategy interface defines a single method PerformOperation(List<int> numbers), ensuring that all operation classes depend only on the methods they need, without unnecessary or bloated interfaces.

### Dependency Inversion Principle (DIP):

What it is: High-level modules should not depend on low-level modules, but both should depend on abstractions (interfaces).

How it's applied: The CalculatorContext class depends on the ICalculatorStrategy interface, not on specific implementations like Addition or Subtraction, etc. This allows for flexible swapping of different operations, reducing tight coupling between the CalculatorContext class and concrete operations such as Addition,Subtraction, Multiplication, or any new that need to be added for extending the app's functionality.

## Design Patterns Used

### Dependency Injection

What it is: Dependency Injection (DI) is a design pattern used to achieve Inversion of Control (IoC) between classes and their dependencies (which was applied by me and explained early). Instead of a class creating its dependencies, they are injected into the class, typically by an external entity like a framework or a container. This approach promotes loose coupling, making the code more modular, testable, and maintainable.
How it's applied: In this project, Dependency Injection is used extensively to manage the dependencies between various components. Here’s a detailed breakdown of how I applied DI:

1.	Service Collection Setup:
•	In the Program.cs file, a ServiceCollection is created to register all the services and their dependencies.
•	Various strategies (AdditionStrategy, SubtractionStrategy, MultiplicationStrategy, DivisionStrategy) are registered as singletons. This means a single instance of each strategy will be created and shared throughout the application. In this case, I do not need to create instances by Request.

  	var serviceCollection = new ServiceCollection()
        .AddSingleton<AdditionStrategy>()
        .AddSingleton<SubtractionStrategy>()
        .AddSingleton<MultiplicationStrategy>()
        .AddSingleton<DivisionStrategy>()
        .AddSingleton<ICalculatorStrategy, AdditionStrategy>()
        .AddSingleton<ICalculatorStrategy, SubtractionStrategy>()
        .AddSingleton<ICalculatorStrategy, MultiplicationStrategy>()
        .AddSingleton<ICalculatorStrategy, DivisionStrategy>()
        .AddSingleton(provider => new Dictionary<string, ICalculatorStrategy>
        {
            { "+", provider.GetService<AdditionStrategy>() },
            { "-", provider.GetService<SubtractionStrategy>() },
            { "*", provider.GetService<MultiplicationStrategy>() },
            { "/", provider.GetService<DivisionStrategy>() }
        })
        .AddSingleton<CalculatorContext>()
        .AddSingleton<ConsoleIO>()
        .BuildServiceProvider();
  	
2.	I used Dynamic Strategy Resolution:
•	A dictionary is used to map operators to their corresponding strategies. This allows for dynamic resolution of strategies based on user input without hardcoding them in a switch statement.

  	.AddSingleton(provider => new Dictionary<string, ICalculatorStrategy>
  {
      { "+", provider.GetService<AdditionStrategy>() },
      { "-", provider.GetService<SubtractionStrategy>() },
      { "*", provider.GetService<MultiplicationStrategy>() },
      { "/", provider.GetService<DivisionStrategy>() }
  })

3.	Service Resolution:
   •	The ConsoleIO and CalculatorContext services are resolved from the service provider. If these services fail to initialize, appropriate error messages are displayed.
    var io = serviceCollection.GetService<ConsoleIO>();
    if (io == null)
    {
        Console.WriteLine("Failed to initialize ConsoleIO service.");
        return;
    }

    var calculatorContext = serviceCollection.GetService<CalculatorContext>();
    if (calculatorContext == null)
    {
        Console.WriteLine("Failed to initialize Calculator service.");
        return;
    }
4.	Setting the Operation Strategy:
•	The CalculatorContext class uses the SetOperatioStrategy method to set the appropriate strategy based on user input. This strategy is then used to perform the calculation.

  	 var strategies = serviceCollection.GetService<Dictionary<string, ICalculatorStrategy>>();
   if (strategies.TryGetValue(operators, out var strategy))
   {
       calculatorContext.SetOperatioStrategy(strategy);
   }
   else
   {
       io.PrintError("Unknown operator. Please, enter a supported operator.");
       continue;
   }

To sum it up, I used DI to manage the creation and resolution of various calculation strategies and other services. By registering these services with the ServiceCollection, the project achieves loose coupling, making it easier to maintain and extend. The use of a dictionary for dynamic strategy resolution further enhances the flexibility of the application.

### Strategy Design Pattern:

What it is: A behavioral design pattern that allows us to define a family of algorithms, encapsulate each one, and make them interchangeable.

How it's applied: Different operations (e.g., addition, subtraction, multiplication, division) are encapsulated into separate classes that implement the ICalculatorStrategy interface. The CalculatorContext class uses an operation strategy, which can be changed at runtime by swapping in a new strategy (operation) via the SetOperatioStrategy method.

Why it's useful: It allows the calculator to support various operations in a flexible and extendable way without modifying the core logic, keeping the code clean and following the Open/Closed Principle.

### Factory Pattern (implicitly):

What it is: A creational design pattern used to create objects without exposing the creation logic to the client.

How it's applied: The factory pattern can be implicitly seen in the way the CalculatorContext instantiates different operations (e.g., Addition, Subtraction). You can further enhance this by using a factory method to dynamically choose the operation based on user input or configuration.

Why it's useful: It centralizes the creation of operation objects, potentially simplifying instantiation and ensuring that the right operation is selected dynamically.

### Why These Principles and Patterns Were Useful?

#### Maintainability: 
By applying SOLID principles, each component (e.g., operation classes) has a clear responsibility, which makes the system easier to maintain and extend. Adding a new operation or modifying an existing one does not require changing the core logic of the Calculator.

#### Extensibility: 
By applying The Open/Closed Principle I can ensure that new operations can be added without altering the existing functionality. This makes the calculator flexible and ready for future extensions, such as supporting more complex mathematical operations.

##### Reusability: 
The Strategy Design Pattern promotes reusability by decoupling the Calculator from specific operations. New operations (e.g., exponentiation, modulus) can be easily integrated by simply creating new concrete classes.

##### Testability: 
The separation of concerns in the design ensures that each part (like operations) can be tested independently. I can write unit tests for individual operations (like addition, subtraction) and mock them if needed in testing the CalculatorContext.

#### Flexibility: 
By applying The Strategy Design Pattern I bring more flexibility in how operations are handled. By allowing the operation to be set dynamically, the CalculatorContext becomes versatile and adaptable to various scenarios without rewriting code.

This approach not only makes my code more modular and clean but also prepares it for future growth, enabling easy testing, debugging, and further extension.
