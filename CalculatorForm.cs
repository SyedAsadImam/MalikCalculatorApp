using System;
using System.Windows.Forms;

namespace SimpleCalculatorApp
{
    public class CalculatorForm : Form
    {
        private TextBox displayBox;
        private Button[] numButtons;
        private Button addButton, subButton, mulButton, divButton, eqButton, clrButton;
        private double result = 0;
        private string operation = string.Empty;
        private bool isOperationPerformed = false;

        public CalculatorForm()
        {
            Text = "Calculator";
            Width = 260;
            Height = 350;

            displayBox = new TextBox
            {
                Left = 10, Top = 10,
                Width = 220, Height = 35,
                ReadOnly = true,
                TextAlign = HorizontalAlignment.Right,
                Font = new System.Drawing.Font("Segoe UI", 18F)
            };
            Controls.Add(displayBox);

            numButtons = new Button[10];
            for (int i = 0; i < 10; i++)
            {
                numButtons[i] = new Button();
                numButtons[i].Text = i.ToString();
                numButtons[i].Width = 50;
                numButtons[i].Height = 40;
                numButtons[i].Click += NumButton_Click;
                Controls.Add(numButtons[i]);
            }

            addButton = CreateOperatorButton("+", 150, 60, OperatorButton_Click);
            subButton = CreateOperatorButton("-", 150, 110, OperatorButton_Click);
            mulButton = CreateOperatorButton("*", 150, 160, OperatorButton_Click);
            divButton = CreateOperatorButton("/", 150, 210, OperatorButton_Click);

            eqButton = new Button { Text = "=", Left = 80, Top = 210, Width = 50, Height = 40 };
            eqButton.Click += EqButton_Click;
            Controls.Add(eqButton);

            clrButton = new Button { Text = "C", Left = 10, Top = 210, Width = 50, Height = 40 };
            clrButton.Click += (s, e) =>
            {
                displayBox.Text = "";
                result = 0;
                operation = string.Empty;
            };
            Controls.Add(clrButton);

            ArrangeButtons();
        }

        private Button CreateOperatorButton(string text, int left, int top, EventHandler click)
        {
            var btn = new Button { Text = text, Left = left, Top = top, Width = 50, Height = 40 };
            btn.Click += click;
            return btn;
        }

        private void ArrangeButtons()
        {
            int xStart = 10, yStart = 60, index = 1;
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                {
                    numButtons[index].Left = xStart + col * 60;
                    numButtons[index].Top = yStart + row * 50;
                    index++;
                }
            numButtons[0].Left = xStart + 60;
            numButtons[0].Top = yStart + 3 * 50;
        }

        private void NumButton_Click(object sender, EventArgs e)
        {
            if (isOperationPerformed) { displayBox.Text = ""; isOperationPerformed = false; }
            displayBox.Text += ((Button)sender).Text;
        }

        private void OperatorButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(displayBox.Text, out double num))
            {
                result = num;
                operation = ((Button)sender).Text;
                isOperationPerformed = true;
            }
        }

        private void EqButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(displayBox.Text, out double secondNum))
            {
                switch (operation)
                {
                    case "+": displayBox.Text = (result + secondNum).ToString(); break;
                    case "-": displayBox.Text = (result - secondNum).ToString(); break;
                    case "*": displayBox.Text = (result * secondNum).ToString(); break;
                    case "/": displayBox.Text = secondNum != 0 ? (result / secondNum).ToString() : "Error"; break;
                }
                isOperationPerformed = true;
            }
        }
    }
}
