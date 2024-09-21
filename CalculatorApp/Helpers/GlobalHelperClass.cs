using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorApp.Helpers
{
    //I declare this class as Internal so that it can only be accessed within the same assembly
    internal class GlobalHelperClass
    {
        //this method are static so that they can be accessed without creating an instance of the class
        public static List<int> ParseInput(string numbers)
        {
            /*
            Requirement#6:Custom logic to support a custom delimiter in the specified format: ///{delimiter}\n{numbers}.
            Explanation of Changes:
            
            1.Custom Delimiter Detection:
            
            - If the input starts with //, the custom delimiter is extracted (the character after //). The rest of the string after \n is treated as the numbers section.
            
            2.Delimiter Replacement:
            
            - Both commas and newlines are replaced with the custom delimiter so that the input can be parsed uniformly, regardless of how the user enters the numbers.
            
            3.Negative Numbers and Values Greater than 1000: 
            
            - Negative numbers are still collected and an exception is thrown if any are found.
            - Numbers greater than 1000 are ignored(converted to 0).
            */

            string delimiter = ","; //default delimiter
            string numbersSection = numbers;

            //Check if the input string starts with a custom delimiter
            if (numbers.StartsWith("//"))
            {
                //examine the input string to extract the custom delimiter and the numbers section
                
                
                int delimiterEndIndex = numbers.IndexOf("\n"); // examine the input string to find the end of the delimiter, \n is the end of the delimiter
                delimiter = numbers[2].ToString(); //Extract custom delimiter(single character)
                //Extract the numbers string
                numbersSection = numbers.Substring(delimiterEndIndex + 1); //extract the numbers section.Number section starts after the new line(\n) character.
            }


            //For adding Support a newline character as an alternative delimiter.I Replace newline characters with detected delimiter to treat them as alternative delimiters.
            numbersSection = numbersSection.Replace("\n", delimiter);

            // Split the numbers section using the detected delimiter
            string[] numbersAsStringArray = numbersSection.Split(delimiter);
            
            
            List<int> numberList = new List<int>();
            var negativeNumbers = new List<int>(); //list to store negative numbers

            //parse the string to get the numbers and add them to the list
            foreach (string numberAsString in numbersAsStringArray) {

                if (string.IsNullOrEmpty(numberAsString))
                {
                    numberList.Add(0); // Treat empty strings as 0
                }
                else if(!int.TryParse(numberAsString, out int result))
                {
                    
                    numberList.Add(0); // Treat invalid numbers as 0
                }
                else
                {
                    if (numberAsString.Contains("-"))
                    {
                        negativeNumbers.Add(int.Parse(numberAsString)); //collect negative numbers
                    }
                    else if (result > 1000)
                    {
                        numberList.Add(0);  // Treat values greater than 1000 as 0
                    }

                    else

                    numberList.Add(result); //add valid numbers to the list
                }

            }
            //if negative numbers are found throw an exception
            if (negativeNumbers.Any())
            {
                throw new Exception("Negatives not allowed: " + string.Join(",", negativeNumbers)); //throw exception with negative numbers if found
            }

            return numberList;
        }
    }
}
