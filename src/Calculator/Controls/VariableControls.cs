namespace Calculator.Controls;

public class VariableControls
    {
        private TextBox _inputField;
        private Button _defineVariableButton;
        private Button _selectVariableButton;
        private ComboBox _selectVariableComboBox;

        
        public VariableControls(TextBox inputField,int height,int width)
        {
            _inputField = inputField;
            _defineVariableButton = new Button();
            _selectVariableButton = new Button();
            _selectVariableComboBox = new ComboBox();

            InitializeControls(height,width);
        }

        public Button[] GetButtons()
        {
            return new[] { _defineVariableButton, _selectVariableButton };
        }

        public ComboBox[] GetComboBoxes()
        {
            return new[] { _selectVariableComboBox };
        }
        private void InitializeControls(int height, int width)
        {
            int buttonWidth = width / 6;
            int buttonHeight = height / 11;
            // Кнопка для определения/изменения переменной
            _defineVariableButton.Text = "Def Var";
            _defineVariableButton.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            _defineVariableButton.Location = new System.Drawing.Point(width/30, 7 * height/9);
            _defineVariableButton.Name = "DefineVariableButton";
            _defineVariableButton.Click += DefineVariableButton_Click;
            
            _selectVariableButton.Text = "Sel Var";
            _selectVariableButton.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            _selectVariableButton.Location = new System.Drawing.Point(width/30 + 3 * buttonWidth / 2, 7 * height/9);
            _selectVariableButton.Name = "SelectVariableButton";
            _selectVariableButton.Click += SelectVariableButton_Click;
            // ComboBox для выбора переменной
            _selectVariableComboBox.Size = new System.Drawing.Size(2 * buttonWidth, buttonHeight);
            _selectVariableComboBox.Location = new System.Drawing.Point(7 * width / 15, 7 * height/9);
            _selectVariableComboBox.Name = "SelectVariableComboBox";
            _selectVariableComboBox.Visible = false;
            _selectVariableComboBox.SelectedIndexChanged += SelectVariableComboBox_SelectedIndexChanged;

            // Обновляем ComboBox при инициализации
        }

        private void DefineVariableButton_Click(object sender, EventArgs e)
        {
            
        }

        private void SelectVariableComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedVariable = comboBox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedVariable))
            {
                _inputField.Text += selectedVariable;
            }

            _selectVariableComboBox.Visible = false;
        }

        private void SelectVariableButton_Click(object sender, EventArgs e)
        {
            // Переключаем видимость ComboBox
            _selectVariableComboBox.Visible = !_selectVariableComboBox.Visible;
        }

    }