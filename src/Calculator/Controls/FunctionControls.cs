using System.Globalization;
using System.Windows.Forms.VisualStyles;
using Calculator.Model;
using Calculator.Presenter;
using Calculator.View;

namespace Calculator.Controls;

public class FunctionControls
{
    private TextBox _inputField;
    private Button _defineFunctionButton;
    private Button _selectFunctionButton;
    private ComboBox _selectFunctionComboBox;

    private Dictionary<string, Function> _functions = new();

    public Dictionary<string, Function> Functions => _functions;
    
    public FunctionControls(TextBox inputField,int buttonHeight,int buttonWidth)
        {
            _inputField = inputField;
            _defineFunctionButton = new Button();
            _selectFunctionButton = new Button();
            _selectFunctionComboBox = new ComboBox();

            InitializeControls(buttonHeight,buttonWidth);
        }

        public Button[] GetButtons()
        {
            return new[] { _defineFunctionButton, _selectFunctionButton };
        }

        public ComboBox[] GetComboBoxes()
        {
            return new[] { _selectFunctionComboBox };
        }
        private void InitializeControls(int height, int width)
        {
            int buttonWidth = width / 6;
            int buttonHeight = height / 11;
            // Кнопка для определения/изменения переменной
            _defineFunctionButton.Text = "Def Func";
            _defineFunctionButton.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            _defineFunctionButton.Location = new System.Drawing.Point(width/30, 8 * height/9);
            _defineFunctionButton.Name = "DefineFunctionButton";
            _defineFunctionButton.Click += DefineFunctionButton_Click;
            
            _selectFunctionButton.Text = "Sel Func";
            _selectFunctionButton.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            _selectFunctionButton.Location = new System.Drawing.Point((3 * buttonWidth / 2 + width/30), 8 * height/9);
            _selectFunctionButton.Name = "SelectFunctionButton";
            _selectFunctionButton.Click += SelectFunctionButton_Click;
            // ComboBox для выбора переменной
            _selectFunctionComboBox.Size = new System.Drawing.Size(2 * buttonWidth, buttonHeight);
            _selectFunctionComboBox.Location = new System.Drawing.Point(7 * width / 15, 8 * height/9 );
            _selectFunctionComboBox.Name = "SelectFunctionComboBox";
            _selectFunctionComboBox.Visible = false;
            _selectFunctionComboBox.SelectedIndexChanged += SelectFunctionComboBox_SelectedIndexChanged;

            // Обновляем ComboBox при инициализации
        }
        private void DefineFunctionButton_Click(object sender, EventArgs e)
        {
            var expression = InputBox.ShowDialog("Format: f(x,y)=x+y", "Input function");

            if (expression is not null)
            {
                try
                {
                    var function = FunctionUtilities.ParseFunction(expression);
                    _functions.Add(function.Name, function);
                    _selectFunctionComboBox.Items.Add(function);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        
        private void SelectFunctionButton_Click(object sender, EventArgs e)
        {
            // Переключаем видимость ComboBox
            _selectFunctionComboBox.Visible = !_selectFunctionComboBox.Visible;
        }
        private void SelectFunctionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem is Function function)
            {
                
                List<string> parameterValues = new();
                foreach (var parameter in function.Parameters)
                {
                    parameterValues.Add(InputParameter(parameter).ToString());
                }

                var functionCall = $"{function.Name}({string.Join(',', parameterValues)})";
                _inputField.Text += functionCall;
            }

            // Скрываем ComboBox после выбора элемента
            _selectFunctionComboBox.Visible = false;
        }

        private int InputParameter(string parameterName)
        {
            string? input = null;
            int value;

            while (string.IsNullOrEmpty(input) || !int.TryParse(input, out value))
            {
                input = InputBox.ShowDialog($"Input {parameterName}", "Input parameter");
            }

            return value;
        }
}