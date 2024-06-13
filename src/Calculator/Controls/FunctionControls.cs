using System.Windows.Forms.VisualStyles;

namespace Calculator.Controls;

public class FunctionControls
{
    private TextBox _inputField;
    private Button _defineFunctionButton;
    private Button _selectFunctionButton;
    private ComboBox _selectFunctionComboBox;
    
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
            
        }
        
        private void SelectFunctionButton_Click(object sender, EventArgs e)
        {
            // Переключаем видимость ComboBox
            _selectFunctionComboBox.Visible = !_selectFunctionComboBox.Visible;
        }
        private void SelectFunctionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedFunction = comboBox.SelectedItem.ToString();
            // Добавляем выбранную функцию в поле ввода или выполняем другие действия по выбору
            _inputField.Text += selectedFunction;

            // Скрываем ComboBox после выбора элемента
            _selectFunctionComboBox.Visible = false;
        }
}