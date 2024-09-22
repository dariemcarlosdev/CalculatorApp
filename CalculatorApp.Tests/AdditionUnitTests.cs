using CalculatorApp.ConcreteOperations;
using CalculatorApp.Interfaces;
using CalculatorApp.Services;

namespace CalculatorApp.Tests
{
    public class AdditionUnitTests
    {
        //This insolation is achieved by creating a new instance of the AdditionStrategy class for each test method.
        AdditionStrategy addition = new ();

        //Test for exceed of maximum number of input numbers of 2
        //[Fact]
        //public void Addition_Of_Exceed_Maximum_Number_Of_Input_Numbers_Of_2_Should_Throw_Exception()
        //{
        //    // Arrange is the first step of the unit test pattern. In this step, I set up the objects, data, and overall environment for the test.
        //    Calculator calculator = new Calculator(addition);
        //    var numbers = "1,2,3";

        //    // Act is the second step of the unit test pattern. In this step, I perform the actual work of the test.
        //    Action act = () => calculator.PerformOperation(numbers);

        //    // Assert is the final step of the unit test pattern. In this step, I verify that the test results are correct, and match the expected results.
        //    Assert.Throws<Exception>(act);
        //}

        //Test for Requirement#2 allowing more than 2 numbers after comment constraint in Calculator.cs, in this case, test Addition_Of_Exceed_Maximum_Number_Of_Input_Numbers_Of_2_Should_Throw_Exception should be commented out, since it is not valid anymore.
        [Fact]
        public void Addition_Of_Allowing_More_Than_2_Numbers_Returns_Correct_Result()
        {
            // Arrange
            CalculatorService calculator = new CalculatorService(addition);
            //replace the Calculator class with CalculatorContext class since the CalculatorContext class is used to manage the calculator context and provides a simple interface to manage the context according to the SRP principle and strategy pattern.
            CalculatorContext calculatorContext = new(addition);
            var numbers = "1,2,3";

            // Act
            var result = calculatorContext.PerformOperation(numbers);

            // Assert
            Assert.Equal("1+2+3=6", result);
        }


        //Test for addition of two numbers
        [Fact]
        public void Addition_Of_Two_Numbers_Returns_Correct_Result()
        {

            //arrange
            //CalculatorService calculator = new CalculatorService(addition);
            CalculatorContext calculatorContext = new(addition);
            string input = "1,2";

            //act
            var result = calculatorContext.PerformOperation(input);

            //Assert
            Assert.Equal("1+2=3", result);
        }

        //Test for empty string or null missing number should be converted to 0
        [Fact]
        public void Addition_Of_Empty_String_Should_Be_Converted_To_0_Returns_Correct_Result()
        {
            // Arrange
            //CalculatorService calculator = new CalculatorService(addition);
            CalculatorContext calculatorContext = new(addition);
            var numbers = "1, ";

            // Act
            var result = calculatorContext.PerformOperation(numbers);

            // Assert
            Assert.Equal("1+0=1", result);
        }


        //Test for negative numbers
        //[Fact]
        //public void Addition_Of_Negative_Numbers_Returns_Correct_Result()
        //{
        //    // Arrange
        //    Calculator calculator = new Calculator(addition);
        //    var numbers = "-3, 4 ";

        //    // Act
        //    var result = calculator.PerformOperation(numbers);

        //    // Assert
        //    Assert.Equal("-3+4=1", result);
        //}

        //Test for invalid number should be converted to 0
        [Fact]
        public void Addition_Of_Invalid_Number_ShouldBe_ConvertedTo_0_Returns_Correct_Result()
        {
            // Arrange
            //CalculatorService calculator = new CalculatorService(addition);
            CalculatorContext calculatorContext = new(addition);
            var numbers = "1,tytyt";

            // Act
            var result = calculatorContext.PerformOperation(numbers);

            // Assert
            Assert.Equal("1+0=1", result);
        }

        //Test for Requirement#3 newline character as an alternative delimiter
        [Fact]
        public void Addition_Of_Newline_Character_As_An_Alternative_Delimiter_Returns_Correct_Result()
        {
            // Arrange
            //CalculatorService calculator = new CalculatorService(addition);
            CalculatorContext calculatorContext = new(addition);
            var numbers = "1\n2,3";

            // Act
            var result = calculatorContext.PerformOperation(numbers);

            // Assert
            Assert.Equal("1+2+3=6", result);
        }

        //Test for Requirement#2 throwing an exception if the numbers are negative. Existing test Addition_Of_Negative_Numbers_Returns_Correct_Result can be commented out, since it is not valid anymore.
        [Fact]
        public void Addition_Of_Negative_Numbers_Should_Throw_Exception()
        {
            // Arrange
            CalculatorService calculator = new CalculatorService(addition);
            CalculatorContext calculatorContext = new(addition);
            var numbers = "-1, -2, -3";

            // Act
            Action act = () => calculatorContext.PerformOperation(numbers);

            // Assert
            Assert.Throws<Exception>(act);
        }

        //Unit test for Requirement#5 numbers greater than 1000 should be treated as 0
        [Fact]
        public void Addition_Of_Numbers_Greater_Than_1000_Should_Be_Treated_As_0_Returns_Correct_Result()
        {
            // Arrange
            CalculatorService calculator = new CalculatorService(addition);
            CalculatorContext calculatorContext = new(addition);
            var numbers = "1001, 2";

            // Act
            var result = calculatorContext.PerformOperation(numbers);

            // Assert
            Assert.Equal("0+2=2", result);
        }

        //Unit test for Requirement#6 custom delimiter of a single character support using format: //{delimiter}\n{numbers}
        [Theory]
        [InlineData("//;\n1;2", "1+2=3")]//Inlinedata attribute is used to pass the input and expected output to the test method
        [InlineData("//#\n2#5", "2+5=7")]
        [InlineData("//.\n2.5", "2+5=7")]
        [InlineData("//,\n2,ff,100", "2+0+100=102")] //Test for custom delimiter (,) and invalid number(ff) should be converted to 0
        public void Addition_Of_Custom_Delimiter_Support_Returns_Correct_Result(string numbers, string expectedResult)
        {
            // Arrange
            //CalculatorService calculator = new CalculatorService(addition);
            CalculatorContext calculatorContext = new(addition);


            // Act
            var result = calculatorContext.PerformOperation(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        //Unit test for Requirment#7 custom delimiter of any length support using format: //[{delimiter}]\n{numbers}
        [Theory]
        [InlineData("//[***]\n1***2***3", "1+2+3=6")]
        [InlineData("//[**]\n1**2**3", "1+2+3=6")]
        [InlineData("//[%%]\n1%%2%%3", "1+2+3=6")]
        [InlineData("//[##]\n1##2##3", "1+2+3=6")]
        [InlineData("//[&&&]\n1&&&2&&&3", "1+2+3=6")]
        [InlineData("//#\n2#5", "2+5=7")] //Test for Requirement#6 custom delimiter of a single character support
        [InlineData("2,10001,6", "2+0+6=8")] //Test for Requirement#5 numbers greater than 1000 should be treated as 0
        public void Addition_Of_Custom_Delimiter_Of_Any_Length_Support_Returns_Correct_Result(string numbers, string expectedResult)
        {
            // Arrange
            //CalculatorService calculator = new CalculatorService(addition);
            CalculatorContext calculatorContext = new(addition);

            // Act
            var result = calculatorContext.PerformOperation(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }


        //Unit test for Requirement#8 multiple custom delimiter of any length support using format: //[{delimiter1}][{delimiter2}]...\n{numbers}
        [Theory]
        [InlineData("//[*][%]\n1*2%3", "1+2+3=6")] //custom delimiters are * and %
        [InlineData("//[*][!!][r9r]\n11r9r22*hh*33!!44", "11+22+0+33+44=110")] //custom delimiters are * and !! and r9r  
        [InlineData("//#\n2#5", "2+5=7")] //Test for Requirement#6 custom delimiter of a single character support. In this case, the delimiter is # and \
        [InlineData("//[##]\n1##2##3", "1+2+3=6")] //Test for Requirement#7 custom delimiter of any length support using format: //[{delimiter}]\n{numbers}. In this case, the delimiter is ##
        [InlineData("//,\n2,ff,100", "2+0+100=102")] //Test for Requirement#6 custom delimiter (,) and invalid number(ff) should be converted to 0. In this case, the delimiter is , and \n2 is the invalid number which should be converted to 0
        [InlineData("2,10001,6", "2+0+6=8")] //Test for Requirement#5 numbers greater than 1000 should be treated as 0. In this case, the number is 10001 which should be treated as 0
        [InlineData("//[***]\n11***22***33", "11+22+33=66")] //Test for multiple characters custom delimiter (***). In this case, the delimiter is ***
        public void Addition_Of_Multiple_Custom_Delimiter_Of_Any_Length_Support_Returns_Correct_Result(string numbers, string expectedResult)
        {
            // Arrange
            //CalculatorService calculator = new CalculatorService(addition);
            CalculatorContext calculatorContext = new(addition);

            // Act
            var result = calculatorContext.PerformOperation(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}