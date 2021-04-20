using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Calculator.CalcFunctions;

namespace Calculator
{
    internal class CalculatorManager
    {

        // Current state
        private CalculationState state;

        // List of all results
        private List<CalculationState> calculationsList;

        public CalculatorManager()
        {
            state = new CalculationState();
            calculationsList = new List<CalculationState>();
        }

        /**
         * Result text containing whole calculation with result according to current state.
         */
        public string GetResultText()
        {
            return GetResultText(state, usingCurrentState: true);
        }

        /**
         * Result text containing whole calculation with result according to given state.
         */
        private string GetResultText(CalculationState state, bool usingCurrentState = false)
        {

            var first = state.FirstNumber;
            var second = state.SecondNumber;
            var operationType = state.OperationType;

            if (first == null || second == null || operationType == null)
            {
                if (usingCurrentState)
                    RestoreCalculationState();

                return "0";
            }

            var operation = GetOperationText(state);
            var result = GetResult((double)first, (double)second, (OperationTypes)operationType);

            var resultOperationText = state.FirstNumber + " " + operation + " " + state.SecondNumber + " = " + result;

            if (usingCurrentState)
                SaveAndRestoreCalculationState();

            return resultOperationText;
        }

        /**
         * Result text containing whole calculation with result according to latest calculation made.
         */
        public string GetLatestResultText()
        {

            var latestState = calculationsList[calculationsList.Count - 1];

            return GetResultText(latestState, usingCurrentState: true);
        }


        /**
         * Whole calculation according to current state.
         */
        public string GetCalculationText()
        {
            var calculation = "";

            if (state.FirstNumber == null)
                return calculation;

            calculation += state.FirstNumber;

            // Check, if number should contain decimal point and it doesn't
            if (state.HasFirstNumberDecimal && !ContainsDecimal((double)state.FirstNumber))
            {
                calculation += ",";
                return calculation;
            }
            

            var operation = GetOperationText(state);
            if (operation == null)
                return calculation;

            // Pridanie operacie k vypoctu
            calculation += " " + operation;

            if (state.SecondNumber == null)
                return calculation;

            // Pridanie druheho cisla k vypoctu
            calculation += " " + state.SecondNumber;

            if (state.HasSecondNumberDecimal && !ContainsDecimal((double)state.SecondNumber))
            {
                calculation += ",";
            }
            

            return calculation;
        }

        private static double GetResult(double firstNumber, double secondNumber, OperationTypes operationType)
        {
            switch (operationType)
            {
                case OperationTypes.ADD: return Add(firstNumber, secondNumber);
                case OperationTypes.SUBTRACT: return Subtract(firstNumber, secondNumber);
                case OperationTypes.MULTIPLY: return Multiply(firstNumber, secondNumber);
                case OperationTypes.DIVIDE: return Divide(firstNumber, secondNumber);
                default: return 0;
            }
        }

        private string GetOperationText(CalculationState state)
        {
            switch (state.OperationType)
            {
                case OperationTypes.ADD: return "+";
                case OperationTypes.SUBTRACT: return "-";
                case OperationTypes.MULTIPLY: return "*";
                case OperationTypes.DIVIDE: return "/";
                default: return null;
            }
        }

        /**
         * <param name="number">Number to concat in calculation.</param>
         */
        public void AddNumber(double number)
        {

            // Check, if first number is set
            if (state.FirstNumber == null)
            {

                // Set new first number
                state.FirstNumber = number;

                return;
            }

            if (state.OperationType == null)
            {

                var first = (double)state.FirstNumber;

                // Check, if number should contain decimal and it doesn't
                if (state.HasFirstNumberDecimal && !ContainsDecimal(first))
                {
                    // Change number to decimal ten
                    number *= 0.1;
                }

                // Update first number
                state.FirstNumber = ConcatNumbers(first, number);

                return;
            }

            var second = state.SecondNumber ?? 0;

            if (state.HasSecondNumberDecimal && !ContainsDecimal(second))
                number *= 0.1;

            state.SecondNumber = ConcatNumbers(second, number);
        }

        public void AddDecimal()
        {
            // Check, if second is set
            if (state.SecondNumber != null)
            {
                state.HasSecondNumberDecimal = true;
                return;
            }

            if (state.FirstNumber != null)
                state.HasFirstNumberDecimal = true;
        }

        public void ChangeOperation(OperationTypes operation)
        {
            state.OperationType = operation;
        }

        /**
         * Clear whole calculation state.
         */
        public void Clear()
        {
            RestoreCalculationState();
        }

        /**
         * Clear latest entry in calculation state
         */
        public void ClearEntry()
        {

            // Remove from last number
            if (state.SecondNumber != null)
            {
                state.SecondNumber = DissociateNumber((double)state.SecondNumber);

                return;
            }

            // Check, if decimal has been removed using short-circuit evaluation
            if (state.SecondNumber != null && !ContainsDecimal((int)state.SecondNumber))
            {

                // Update state
                state.HasSecondNumberDecimal = false;
            }

            // Remove operation
            if (state.OperationType != null)
            {
                state.OperationType = null;
                return;
            }

            // Remove from first number
            if (state.FirstNumber != null)
                state.FirstNumber = DissociateNumber((double)state.FirstNumber);

            // Update state of first number
            if (state.FirstNumber != null && !ContainsDecimal((int)state.FirstNumber))
                state.HasFirstNumberDecimal = false;

        }

        private void SaveAndRestoreCalculationState()
        {
            calculationsList.Add(state);
            RestoreCalculationState();
        }

        private void RestoreCalculationState()
        {
            state = new CalculationState();
        }

    }
}
