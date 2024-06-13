using Calculator.Controls;
namespace Calculator.View;

public partial class CalculatorForm : Form
{
    private TextBox _inputField;
    private NumberControls _numberButtons;
    private OperationControls _operationButtons;
    private VariableControls _variableControls;
    private FunctionControls _functionControls;
    private Button _calculateButton;
    private Button _resetButton;
    public CalculatorForm()
    {
        int windowWidth = 300;
        int windowHeight = 450;
        InitializeComponent(windowWidth,windowHeight);
        string[] numberTexts = { "7", "8", "9", "4", "5", "6", "1", "2", "3", "0" };
        string[] operationTexts = { "+", "-", "*", "/", "(",")" };
        // Создание экземпляра NumberButtons с передачей обработчика события Click
        _numberButtons = new NumberControls(_inputField, windowWidth, windowHeight, numberTexts);
        Button[] numberButtons = _numberButtons.GetButtons();
        
        _operationButtons = new OperationControls(_inputField, windowWidth, windowHeight, operationTexts);
        Button[] operButtons = _operationButtons.GetButtons();

        _variableControls = new VariableControls(_inputField, windowHeight, windowWidth);

        _functionControls = new FunctionControls(_inputField, windowHeight, windowWidth);
        
        Button[] varButtons = _variableControls.GetButtons();
        ComboBox[] varComboBoxes = _variableControls.GetComboBoxes();
        
        Button[] funcButtons = _functionControls.GetButtons();
        ComboBox[] funcComboBoxes = _functionControls.GetComboBoxes();
        // Добавление кнопок на форму
        foreach (var button in numberButtons)
        {
            Controls.Add(button);
        }

        foreach (var button in operButtons)
        {
            Controls.Add(button);
        }

        foreach (var button in varButtons)
        {
            Controls.Add(button);
        }

        foreach (var comboBox in varComboBoxes)
        {
            Controls.Add(comboBox);
        }
        
        foreach (var button in funcButtons)
        {
            Controls.Add(button);
        }

        foreach (var comboBox in funcComboBoxes)
        {
            Controls.Add(comboBox);
        }
        
        
    }
    
    // Обработчик для кнопки вычисления выражения
    private void CalculateButton_Click(object sender, EventArgs e)
    {
        // Логика вычисления выражения
    }

    // Обработчик для кнопки сброса выражения
    private void ResetButton_Click(object sender, EventArgs e)
    {
        _inputField.Text = string.Empty;
    }
}