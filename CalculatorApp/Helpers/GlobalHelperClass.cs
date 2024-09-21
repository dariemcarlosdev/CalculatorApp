using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Helpers
{
    //I declare this class as Internal so that it can only be accessed within the same assembly
    internal class GlobalHelperClass
    {
        //this method are static so that they can be accessed without creating an instance of the class
        public static List<int> ParseInput(string numbers)
        {
            //Custom logic for parsing based on delimiter Handle commas

            //For adding Support a newline character as an alternative delimiter.I Replace newline characters with commas to treat them as alternative delimiters.
            numbers = numbers.Replace("\n", ",");

            //Split the input string based on comma.
            string[] numberArray = numbers.Split(',');
            
            
            List<int> numberList = new List<int>();
            var negativeNumbers = new List<int>(); //list to store negative numbers

            //parse the string to get the numbers and add them to the list
            foreach (string number in numberArray) {

                if (string.IsNullOrEmpty(number))
                {
                    numberList.Add(0);
                }
                else if(!int.TryParse(number, out int result))
                {
                    if (number.Contains("-"))
                    {
                        negativeNumbers.Add(int.Parse(number)); //collect negative numbers
                    }
                    
                    numberList.Add(0);
                }
                else
                {
                    if (number.Contains("-"))
                    {
                        negativeNumbers.Add(int.Parse(number)); //collect negative numbers
                    }
                    numberList.Add(result);
                }

            }
            //if negative numbers are found throw an exception
            if (negativeNumbers.Any())
            {
                throw new Exception("Negatives not allowed: " + string.Join(",", negativeNumbers));
            }

            return numberList;
        }
    }
}
