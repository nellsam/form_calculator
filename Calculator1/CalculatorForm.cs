using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {

        private readonly CalculatorManager _calculatorManager;

        public CalculatorForm()
        {
            InitializeComponent();

            _calculatorManager = new CalculatorManager();

            SetHandlers();

            AllocConsole();
        }

        private void SetHandlers()
        {
            zeroButton.Click += HandleClick;
            oneButton.Click += HandleClick;
            twoButton.Click += HandleClick;
            threeButton.Click += HandleClick;
            fourButton.Click += HandleClick;
            fiveButton.Click += HandleClick;
            sixButton.Click += HandleClick;
            sevenButton.Click += HandleClick;
            eightButton.Click += HandleClick;
            nineButton.Click += HandleClick;
            addButton.Click += HandleClick;
            subtractButton.Click += HandleClick;
            multiplyButton.Click += HandleClick;
            divideButton.Click += HandleClick;
            clearButton.Click += HandleClick;
            resultButton.Click += HandleClick;
            clearEntryButton.Click += HandleClick;
            decimalButton.Click += HandleClick;
            //KeyDown += Form1_KeyDown;
        }

        private void HandleClick(object sender, EventArgs e)
        {
            if (!(sender is Button)) return;

            var button = (Button)sender;

            switch (button.Name)
            {
                case "zeroButton":
                    AddNumber(0);
                    break;
                case "oneButton":
                    AddNumber(1);
                    break;
                case "twoButton":
                    AddNumber(2);
                    break;
                case "threeButton":
                    AddNumber(3);
                    break;
                case "fourButton":
                    AddNumber(4);
                    break;
                case "fiveButton":
                    AddNumber(5);
                    break;
                case "sixButton":
                    AddNumber(6);
                    break;
                case "sevenButton":
                    AddNumber(7);
                    break;
                case "eightButton":
                    AddNumber(8);
                    break;
                case "nineButton":
                    AddNumber(9);
                    break;
                case "addButton":
                    ChangeOperation(OperationTypes.ADD);
                    break;
                case "subtractButton":
                    ChangeOperation(OperationTypes.SUBTRACT);
                    break;
                case "multiplyButton":
                    ChangeOperation(OperationTypes.MULTIPLY);
                    break;
                case "divideButton":
                    ChangeOperation(OperationTypes.DIVIDE);
                    break;
                case "clearButton":
                    Clear();
                    break;
                case "clearEntryButton":
                    ClearEntry();
                    break;
                case "decimalButton":
                    AddDecimal();
                    break;
                case "resultButton":
                    DisplayResult();
                    break;

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                    zeroButton.PerformClick();
                    break;
                case Keys.D1:
                    oneButton.PerformClick();
                    break;
                case Keys.D2:
                    twoButton.PerformClick();
                    break;
                case Keys.D3:
                    threeButton.PerformClick();
                    break;
                case Keys.D4:
                    fourButton.PerformClick();
                    break;
                case Keys.D5:
                    fiveButton.PerformClick();
                    break;
                case Keys.D6:
                    sixButton.PerformClick();
                    break;
                case Keys.D7:
                    sevenButton.PerformClick();
                    break;
                case Keys.D8:
                    eightButton.PerformClick();
                    break;
                case Keys.D9:
                    nineButton.PerformClick();
                    break;
                case Keys.NumPad0:
                    zeroButton.PerformClick();
                    break;
                case Keys.NumPad1:
                    oneButton.PerformClick();
                    break;
                case Keys.NumPad2:
                    twoButton.PerformClick();
                    break;
                case Keys.NumPad3:
                    threeButton.PerformClick();
                    break;
                case Keys.NumPad4:
                    fourButton.PerformClick();
                    break;
                case Keys.NumPad5:
                    fiveButton.PerformClick();
                    break;
                case Keys.NumPad6:
                    sixButton.PerformClick();
                    break;
                case Keys.NumPad7:
                    sevenButton.PerformClick();
                    break;
                case Keys.NumPad8:
                    eightButton.PerformClick();
                    break;
                case Keys.NumPad9:
                    nineButton.PerformClick();
                    break;
                case Keys.Decimal:
                    decimalButton.PerformClick();
                    break;
                case Keys.Enter:
                    resultButton.PerformClick();
                    break;
                case Keys.Back:
                    clearEntryButton.PerformClick();
                    break;
                case Keys.Delete:
                    clearButton.PerformClick();
                    break;
                case Keys.Add:
                    addButton.PerformClick();
                    break;
                case Keys.Subtract:
                    subtractButton.PerformClick();
                    break;
                case Keys.Multiply:
                    multiplyButton.PerformClick();
                    break;
                case Keys.Divide:
                    divideButton.PerformClick();
                    break;
            }

            // Remove focus from button
            Focus();
           e.Handled = true;
        }

        private void AddNumber(int number)
        {
            _calculatorManager.AddNumber(number);
            resultText.Text = _calculatorManager.GetCalculationText();
        }

        private void ChangeOperation(OperationTypes operation)
        {
            _calculatorManager.ChangeOperation(operation);
            UpdateCalculationText();
        }

        private void DisplayResult()
        {
            resultText.Text = _calculatorManager.GetResultText();
            UpdateCalculationHistory();
        }

        private void Clear()
        {
            _calculatorManager.Clear();
            resultText.Text = _calculatorManager.GetCalculationText();
        }

        private void ClearEntry()
        {
            Console.WriteLine("Clear entry called");
            _calculatorManager.ClearEntry();
            resultText.Text = _calculatorManager.GetCalculationText();
        }

        private void AddDecimal()
        {
            _calculatorManager.AddDecimal();
            UpdateCalculationText();
        }

        private void UpdateCalculationText()
        {
            resultText.Text = _calculatorManager.GetCalculationText();
        }

        private void UpdateCalculationHistory()
        {
            var latestResult = _calculatorManager.GetLatestResultText();
            calculationsList.Items.Insert(0, latestResult);
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            Focus();
        }
    }
}

