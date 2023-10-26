namespace Converter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            ConvertButton = new Button();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaptionText;
            label1.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(55, 63);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(133, 25);
            label1.TabIndex = 0;
            label1.Text = "Enter Xml File";
            // 
            // button1
            // 
            button1.Location = new Point(352, 106);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(148, 27);
            button1.TabIndex = 1;
            button1.Text = "browse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Control;
            textBox1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.ForeColor = Color.DimGray;
            textBox1.Location = new Point(41, 110);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(259, 25);
            textBox1.TabIndex = 4;
            textBox1.Text = "Filepath...";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // ConvertButton
            // 
            ConvertButton.Cursor = Cursors.Hand;
            ConvertButton.ForeColor = SystemColors.Highlight;
            ConvertButton.Location = new Point(48, 182);
            ConvertButton.Margin = new Padding(4, 3, 4, 3);
            ConvertButton.Name = "ConvertButton";
            ConvertButton.Size = new Size(162, 27);
            ConvertButton.TabIndex = 3;
            ConvertButton.Text = "Convert";
            ConvertButton.UseVisualStyleBackColor = true;
            ConvertButton.Click += ConvertButton_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.Control;
            textBox2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.ForeColor = Color.DimGray;
            textBox2.Location = new Point(41, 140);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(332, 25);
            textBox2.TabIndex = 4;
            textBox2.Text = "SaveLocation...";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(611, 390);
            Controls.Add(textBox2);
            Controls.Add(ConvertButton);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            Margin = new Padding(4, 3, 4, 3);
            MaximumSize = new Size(900, 900);
            MinimumSize = new Size(500, 400);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private OpenFileDialog openFileDialog1;
        private Button ConvertButton;
        private TextBox textBox2;
    }
}

