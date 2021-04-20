
namespace Calculator
{
    class CalculationState
    {
        public double? FirstNumber { get; set; }
        public bool HasFirstNumberDecimal { get; set; }
        public double? SecondNumber { get; set; }
        public bool HasSecondNumberDecimal { get; set; }

        public OperationTypes? OperationType { get; set; }

        public CalculationState()
        {
            FirstNumber = null;
            HasFirstNumberDecimal = false;

            SecondNumber = null;
            HasSecondNumberDecimal = false;

            OperationType = null;
        }
    }
}
