using EventHandler = System.EventHandler;

namespace Calculator.View;

partial class CalculatorForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }
    // Обработчик для числовых кнопок
    
    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent(int windowWidth,int windowHeght)
    {
        this.components = new System.ComponentModel.Container();
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.ClientSize = new System.Drawing.Size(windowWidth, windowHeght);
    this.Text = "Calculator";
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;

    // Поле для ввода выражений
    _inputField = new TextBox
    {
        Location = new System.Drawing.Point(windowWidth/30, windowHeght/45),
        Size = new System.Drawing.Size(14 * windowWidth/15, windowHeght/15),
        Name = "InputField"
    };
    this.Controls.Add(_inputField);

    int buttonWidth = windowWidth / 6;
    int buttonHeight = windowHeght / 11;
    // Кнопка для вычисления выражения
    _calculateButton = new Button
    {
        Text = "=",
        Size = new System.Drawing.Size(buttonWidth, buttonHeight),
        Location = new System.Drawing.Point(8 * windowWidth/10, windowHeght/9 + windowHeght/10),
        Name = "CalculateButton"
    };
    _calculateButton.Click += new EventHandler(CalculateButton_Click);
    this.Controls.Add(_calculateButton);

    // Кнопка для сброса выражения
    _resetButton = new Button
    {
        Text = "C",
        Size = new System.Drawing.Size(buttonWidth, buttonHeight),
        Location = new System.Drawing.Point(8 * windowWidth/10, windowHeght/9),
        Name = "ResetButton"
    };
    _resetButton.Click += new EventHandler(ResetButton_Click);
    this.Controls.Add(_resetButton);
    }

    #endregion
}