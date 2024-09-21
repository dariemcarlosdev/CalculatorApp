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
        public static List<int> ParseInput(string numbersAsStringInput)
        {
   

            string delimiter = ","; //default delimiter
            string numbersAsStringSection = numbersAsStringInput;


            /* Requirement#7:
            
            Custom logic to support a custom delimiter of any length in the specified format: //[{delimiter}]\n{numbers}.

            Explanation of Changes:
            
            1. Multi-Character Delimiter Detection:
            - The method now checks if the input starts with //[ and includes ]\n to identify a custom delimiter of any length.
            - The delimiter is extracted by slicing the string between //[ and ]\n.
            
            2. Handling Single Character Delimiter:
            - The previous single-character delimiter handling remains for backward compatibility.
            
            3. String Split:
            The Split method now uses the detected delimiter (whether single or multi-character) to split the numbers section.
             */

            //Check if a custom delimiter of any length is provided and defined in the format //[{delimiter}]\n{numbers}
            if (numbersAsStringInput.StartsWith("//[") && numbersAsStringInput.Contains("]\n"))
            {
                // Extract the custom delimiter
                int delimiterEndIndex = numbersAsStringInput.IndexOf("]\n"); // examine the input string to find the end of the delimiter, ]\n is the end of the delimiter
                delimiter = numbersAsStringInput.Substring(3, delimiterEndIndex - 3); //Extract custom delimiter
                //Extract the numbers string
                numbersAsStringSection = numbersAsStringInput.Substring(delimiterEndIndex + 2); //extract the numbers section.Number section starts after the new line(\n) character.
            }

            /* Requirement#6:
            
            Custom logic to support a custom single delimiter in the specified format: ///{delimiter}\n{numbers}.
               
            Explanation of Changes:
            
            1. Custom Delimiter Detection:
            - If the input starts with //, the custom delimiter is extracted (the character after //). The rest of the string after \n is treated as the numbers section.

            2. Delimiter Replacement:
            - Both commas and newlines are replaced with the custom delimiter so that the input can be parsed uniformly, regardless of how the user enters the numbers.

            3. Negative Numbers and Values Greater than 1000: 
            - Negative numbers are still collected and an exception is thrown if any are found.
            - Numbers greater than 1000 are ignored(converted to 0).
             */

            //Check if the input string starts with a custom delimiter
            else if (numbersAsStringInput.StartsWith("//"))
            {
                // Extract the single-character custom delimiter
                int delimiterEndIndex = numbersAsStringInput.IndexOf("\n"); // examine the input string to find the end of the delimiter, \n is the end of the delimiter
                delimiter = numbersAsStringInput[2].ToString(); //Extract custom delimiter(single character)
                //Extract the numbers string
                numbersAsStringSection = numbersAsStringInput.Substring(delimiterEndIndex + 1); //extract the numbers section.Number section starts after the new line(\n) character.
            }


            //For adding Support a newline character as an alternative delimiter.I Replace newline characters with detected delimiter to treat them as alternative delimiters.
            numbersAsStringSection = numbersAsStringSection.Replace("\n", delimiter);

            // Split the numbers section using the detected delimiter
            string[] numbersAsStringArray = numbersAsStringSection.Split(delimiter);
            
            
            List<int> numberList = new List<int>();
            var negativeNumbersList = new List<int>(); //list to store negative numbers

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
                        negativeNumbersList.Add(int.Parse(numberAsString)); //collect negative numbers
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
            if (negativeNumbersList.Any())
            {
                throw new Exception("Negatives not allowed: " + string.Join(",", negativeNumbersList)); //throw exception with negative numbers if found
            }

            return numberList;
        }
    }
}
