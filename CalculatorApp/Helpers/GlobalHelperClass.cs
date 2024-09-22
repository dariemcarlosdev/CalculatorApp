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

            /* Requirement#8:
            New Changes added:
            1. Allow the Add method to handle multiple delimiters.
            Multiple Delimiters Detection:The method checks for multiple custom delimiters enclosed in square brackets ([ ]).
            It uses a regular expression to extract all delimiters and adds them to the list.


            2. Delimiter Replacement and Splitting:
            The Split method uses all extracted delimiters to split the numbers section.
            Both single and multiple custom delimiters are supported.

            3. Negative Numbers and Values Greater than 1000:
            Negative numbers are collected, and an exception is thrown if any are found.
            Numbers greater than 1000 are ignored (converted to 0).
             */

            //to handle multiple delimiters, now I create a list of delimiters instead of a single delimiter.
            //string delimiter = ","; //default delimiter
            var delimiters = new List<string> { "," }; //default delimiters is a comma
            string numbersAsStringSection = numbersAsStringInput;//default numbers section

            // Check if a custom delimiter or multiple delimiters are defined using //[{delimiter}][{delimiter2}]\n format
            if (numbersAsStringInput.StartsWith("//") && numbersAsStringInput.Contains("\n"))
            {
                // Extract the custom delimiter(s) between // and \n
                int delimiterEndIndex = numbersAsStringInput.IndexOf("\n"); // Find the end of the delimiter(s) section
                string delimiterSection = numbersAsStringInput.Substring(2, delimiterEndIndex - 2); // Extract the delimiter(s) section
                numbersAsStringSection = numbersAsStringInput.Substring(delimiterEndIndex + 1); // Extract the numbers section

                // Check if we have multiple delimiters enclosed in square brackets
                if (delimiterSection.StartsWith("[") && delimiterSection.EndsWith("]"))
                {
                    var delimiterMatches = System.Text.RegularExpressions.Regex.Matches(delimiterSection, @"\[(.*?)\]"); // Use regex to extract delimiters enclosed in square brackets
                    foreach (var match in delimiterMatches)
                    {
                        
                        delimiters.Add(match.ToString().Trim('[', ']')); // Add the extracted delimiters inside brackets to the list of delimiters to be used for splitting the numbers section
                    }
                }

                else
                {
                    //if only a single delimiter is provided, add it to the list of delimiters, backward compatibility
                    delimiters.Add(delimiterSection); // Add the single delimiter to the list of delimiters to be used for splitting the numbers section
                }

            }

            // Replace newline characters with a default comma delimiter to treat them as alternative delimiters
            numbersAsStringSection = numbersAsStringSection.Replace("\n", ",");

            // Use the extracted delimiters (including multi-character ones) to split the numbers section
            var numbersAsStringArray = numbersAsStringSection.Split(delimiters.ToArray(), StringSplitOptions.None); // Split the numbers section using the delimiters. StringSplitOptions.None is used to keep empty strings as entries, since they are treated as 0, acording to the requirements
           
            
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
