using System;

namespace Calculator
{
    internal class CalcFunctions
    {

        private const string DEC_POINT = ",";

        public static double Add(double number1, double number2)
        {
            return number1 + number2;
        }

        public static double Subtract(double number1, double number2)
        {
            return number1 - number2;
        }

        public static double Multiply(double number1, double number2)
        {
            return number1 * number2;
        }

        
        public static double Divide(double number1, double number2)
        {
            return number1 / number2;
        }

        /**
         * Concatenation of given numbers.
         * Note that second number can contain decimal point and it's handled.
         */
        public static double ConcatNumbers(double number1, double number2)
        {
            try
            {

                if (ContainsDecimal(number1) && ContainsDecimal(number2))
                    throw new Exception("Cannot concatenate two numbers with decimal point");

                // Convert numbers to string
                var firstNumberText = number1.ToString();
                var secondNumberText = number2.ToString();

                if (ContainsDecimal(number2))
                {
                    var decimalIndex = secondNumberText.IndexOf(DEC_POINT);

                    // Substring after decimal point
                    secondNumberText = secondNumberText.Substring(decimalIndex, secondNumberText.Length - 1);
                }

                // Concatenate text
                var numberText = firstNumberText + secondNumberText;

                // Convert back to double
                var newNumber = Convert.ToDouble(numberText);

                return newNumber;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Unexpected format exception " + e);
                return 0;
            }
        }

        /**
         * <summary>Remove last number entry from whole number.</summary>
         * Null is returned, if number is empty.
         */
        public static double? DissociateNumber(double number)
        {
            try
            {
                // Convert to string
                var numberText = number.ToString();

                // Remove last char
                numberText = numberText.Remove(numberText.Length - 1);

                // Check, if whole number is removed
                if (numberText.Length == 0) return null;

                // Make sure number doesn't end with decimal point
                if (numberText[numberText.Length - 1].ToString() == DEC_POINT)
                {
                    numberText = numberText.Remove(numberText.Length - 1);
                }

                // Convert back to int
                var newNumber = Convert.ToDouble(numberText);

                return newNumber;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Unexpected format exception " + e);
                return 0;
            }
        }

        public static bool ContainsDecimal(double number)
        {
            return number.ToString().Contains(DEC_POINT);
        }
    }
}
