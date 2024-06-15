namespace Calculator.Controls;

public class NumberControls
{
    private Button[] _numberButtons;
    private TextBox _inputField;

    public NumberControls(TextBox inputField, int width, int height, string[] numberTexts)
    {
        _inputField = inputField;
        _numberButtons = new Button[numberTexts.Length];
        int buttonWidth = width / 6;
        int buttonHeight = height / 11;
        for (int i = 0; i < _numberButtons.Length; i++)
        {
            _numberButtons[i] = new Button
            {
                Text = numberTexts[i],
                Size = new Size(buttonWidth, buttonHeight),
                Location = new Point(10 + (i % 3) * (buttonWidth + 5), 50 + (i / 3) * (buttonHeight + 5)),
                Name = "Button" + numberTexts[i]
            };
            _numberButtons[i].Click += new EventHandler(NumberButton_Click);
        }
    }

    public Button[] GetButtons()
    {
        return _numberButtons;
    }
    
    private void NumberButton_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        _inputField.Text += button.Text;
    }

}