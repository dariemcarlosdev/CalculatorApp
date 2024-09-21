using CalculatorApp.ConcreteOperations;
using CalculatorApp.Interfaces;
using CalculatorApp.Services;

namespace CalculatorApp.Tests
{
    public class AdditionUnitTests
    {
        Addition addition = new ();

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
            Calculator calculator = new Calculator(addition);
            var numbers = "1,2,3";

            // Act
            var result = calculator.PerformOperation(numbers);

            // Assert
            Assert.Equal("1+2+3=6", result);
        }


        //Test for addition of two numbers
        [Fact]
        public void Addition_Of_Two_Numbers_Returns_Correct_Result()
        {
            Calculator calculator = new Calculator(addition);
            //arrange
            string input = "1,2";

            //act
            var result = calculator.PerformOperation(input);

            //Assert
            Assert.Equal("1+2=3", result);
        }

        //Test for empty string or null missing number should be converted to 0
        [Fact]
        public void Addition_Of_Empty_String_Should_Be_Converted_To_0_Returns_Correct_Result()
        {
            // Arrange
            Calculator calculator = new Calculator(addition);
            var numbers = "1, ";

            // Act
            var result = calculator.PerformOperation(numbers);

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
            Calculator calculator = new Calculator(addition);
            var numbers = "1,tytyt";

            // Act
            var result = calculator.PerformOperation(numbers);

            // Assert
            Assert.Equal("1+0=1", result);
        }

        //Test for Requirement#3 newline character as an alternative delimiter
        [Fact]
        public void Addition_Of_Newline_Character_As_An_Alternative_Delimiter_Returns_Correct_Result()
        {
            // Arrange
            Calculator calculator = new Calculator(addition);
            var numbers = "1\n2,3";

            // Act
            var result = calculator.PerformOperation(numbers);

            // Assert
            Assert.Equal("1+2+3=6", result);
        }

        //Test for Requirement#2 throwing an exception if the numbers are negative. Existing test Addition_Of_Negative_Numbers_Returns_Correct_Result can be commented out, since it is not valid anymore.
        [Fact]
        public void Addition_Of_Negative_Numbers_Should_Throw_Exception()
        {
            // Arrange
            Calculator calculator = new Calculator(addition);
            var numbers = "-1, -2, -3";

            // Act
            Action act = () => calculator.PerformOperation(numbers);

            // Assert
            Assert.Throws<Exception>(act);
        }

        //Unit test for Requirement# numbers greater than 1000 should be treated as 0
        [Fact]
        public void Addition_Of_Numbers_Greater_Than_1000_Should_Be_Treated_As_0_Returns_Correct_Result()
        {
            // Arrange
            Calculator calculator = new Calculator(addition);
            var numbers = "1001, 2";

            // Act
            var result = calculator.PerformOperation(numbers);

            // Assert
            Assert.Equal("0+2=2", result);
        }   
    }
}