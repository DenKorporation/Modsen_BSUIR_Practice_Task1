namespace Calculator.Controls;

public class OperationControls
{
    private Button[] _operationButtons;
    private TextBox _inputField;

    public OperationControls(TextBox inputField, int width, int height, string[] operationTexts)
    {
        _inputField = inputField;
        _operationButtons = new Button[operationTexts.Length];
        int buttonHeight = height / 11;
        int buttonWidth = width / 6;
        for (int i = 0; i < _operationButtons.Length; i++)
        {
            _operationButtons[i] = new Button
            {
                Text = operationTexts[i],
                Size = new System.Drawing.Size(buttonWidth, buttonHeight),
                Location = new System.Drawing.Point(18 * width/30, 50 + i * (buttonHeight + 5)),
                Name = "Button" + operationTexts[i]
            };
            _operationButtons[i].Click += new EventHandler(OperationButton_Click);
        }
    }

    public Button[] GetButtons()
    {
        return _operationButtons;
    }
    
    private void OperationButton_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        _inputField.Text += button.Text;
    }
}