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
            browseButton = new Button();
            inputPath = new TextBox();
            ConvertButton = new Button();
            savePath = new TextBox();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            button1 = new Button();
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
            // browseButton
            // 
            browseButton.Location = new Point(352, 106);
            browseButton.Margin = new Padding(4, 3, 4, 3);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(148, 27);
            browseButton.TabIndex = 1;
            browseButton.Text = "browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // inputPath
            // 
            inputPath.BackColor = SystemColors.Control;
            inputPath.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            inputPath.ForeColor = Color.DimGray;
            inputPath.Location = new Point(41, 110);
            inputPath.Margin = new Padding(4, 3, 4, 3);
            inputPath.Name = "inputPath";
            inputPath.ReadOnly = true;
            inputPath.Size = new Size(259, 25);
            inputPath.TabIndex = 4;
            inputPath.Text = "Filepath...";
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
            // savePath
            // 
            savePath.BackColor = SystemColors.Control;
            savePath.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            savePath.ForeColor = Color.DimGray;
            savePath.Location = new Point(41, 140);
            savePath.Margin = new Padding(4, 3, 4, 3);
            savePath.Name = "savePath";
            savePath.ReadOnly = true;
            savePath.Size = new Size(332, 25);
            savePath.TabIndex = 4;
            savePath.Text = "SaveLocation...";
            // 
            // button1
            // 
            button1.Location = new Point(321, 238);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(611, 390);
            Controls.Add(button1);
            Controls.Add(savePath);
            Controls.Add(ConvertButton);
            Controls.Add(inputPath);
            Controls.Add(browseButton);
            Controls.Add(label1);
            Margin = new Padding(4, 3, 4, 3);
            MaximumSize = new Size(900, 900);
            MinimumSize = new Size(500, 400);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button browseButton;
        private TextBox inputPath;
        private Button ConvertButton;
        private TextBox savePath;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Button button1;
    }
}

