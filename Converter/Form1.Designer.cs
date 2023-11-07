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
			Title = new Label();
			browseButton = new Button();
			inputPath = new TextBox();
			ConvertButton = new Button();
			savePath = new TextBox();
			openFileDialog = new OpenFileDialog();
			saveFileDialog = new SaveFileDialog();
			label1 = new Label();
			label2 = new Label();
			template_html = new OpenFileDialog();
			SuspendLayout();
			// 
			// Title
			// 
			Title.AutoSize = true;
			Title.BackColor = SystemColors.ActiveCaptionText;
			Title.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
			Title.Location = new Point(28, 9);
			Title.Margin = new Padding(4, 0, 4, 0);
			Title.Name = "Title";
			Title.Size = new Size(142, 25);
			Title.TabIndex = 0;
			Title.Text = "Enter Json File";
			// 
			// browseButton
			// 
			browseButton.Location = new Point(425, 94);
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
			inputPath.Location = new Point(28, 65);
			inputPath.Margin = new Padding(4, 3, 4, 3);
			inputPath.Name = "inputPath";
			inputPath.ReadOnly = true;
			inputPath.Size = new Size(345, 25);
			inputPath.TabIndex = 4;
			inputPath.Text = "Filepath...";
			// 
			// ConvertButton
			// 
			ConvertButton.Cursor = Cursors.Hand;
			ConvertButton.ForeColor = SystemColors.Highlight;
			ConvertButton.Location = new Point(47, 262);
			ConvertButton.Margin = new Padding(4, 3, 4, 3);
			ConvertButton.Name = "ConvertButton";
			ConvertButton.Size = new Size(160, 27);
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
			savePath.Location = new Point(28, 96);
			savePath.Margin = new Padding(4, 3, 4, 3);
			savePath.Name = "savePath";
			savePath.ReadOnly = true;
			savePath.Size = new Size(345, 25);
			savePath.TabIndex = 4;
			savePath.Text = "SaveLocation...";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.BackColor = SystemColors.ActiveCaptionText;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(28, 159);
			label1.Name = "label1";
			label1.Size = new Size(360, 21);
			label1.TabIndex = 5;
			label1.Text = "The Pdf file will be created using the Template html";
			label1.Visible = false;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.BackColor = SystemColors.ActiveCaptionText;
			label2.Cursor = Cursors.Hand;
			label2.FlatStyle = FlatStyle.Popup;
			label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(28, 195);
			label2.Name = "label2";
			label2.Size = new Size(237, 21);
			label2.TabIndex = 6;
			label2.Text = "To Choose Custom one Clickhere";
			label2.Visible = false;
			label2.Click += label2_Click;
			// 
			// template_html
			// 
			template_html.InitialDirectory = " Environment.CurrentDirectory";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ActiveCaption;
			ClientSize = new Size(614, 361);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(savePath);
			Controls.Add(ConvertButton);
			Controls.Add(inputPath);
			Controls.Add(browseButton);
			Controls.Add(Title);
			Margin = new Padding(4, 3, 4, 3);
			MaximumSize = new Size(900, 900);
			MinimumSize = new Size(630, 400);
			Name = "Form1";
			Text = "Form1";
			Load += Form1_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label Title;
		private Button browseButton;
		private TextBox inputPath;
		private Button ConvertButton;
		private TextBox savePath;
		private OpenFileDialog openFileDialog;
		private SaveFileDialog saveFileDialog;
		private Label label1;
		private Label label2;
		private OpenFileDialog template_html;
	}
}

