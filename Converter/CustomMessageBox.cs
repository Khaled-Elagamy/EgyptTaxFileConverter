using System.Net;

namespace Converter
{
	public partial class CustomMessageBox : Form
	{
		public CustomMessageBox(string message, string linkText, string linkUrl)
		{
			InitializeComponent();
			InitializeControls(message, linkText, linkUrl);
		}

		private void InitializeControls(string message, string linkText, string linkUrl)
		{
			Text = "Custom Message Box";
			Size = new System.Drawing.Size(400, 150);
			StartPosition = FormStartPosition.CenterParent;
			FormBorderStyle = FormBorderStyle.FixedDialog;

			Label labelMessage = new Label
			{
				Text = message,
				Location = new System.Drawing.Point(20, 20),
				AutoSize = true,
			};
			Controls.Add(labelMessage);

			Button DownloadButton = new Button
			{
				Text = "Download",
				Location = new System.Drawing.Point(110, 80),
				Size = new System.Drawing.Size(80, 30),
			};
			DownloadButton.Click += DownloadButton_Click;

			Controls.Add(DownloadButton);

			Button closeButton = new Button
			{
				Text = "Close",
				Location = new System.Drawing.Point(200, 80),
				Size = new System.Drawing.Size(80, 30),
			};
			closeButton.Click += (sender, e) => Close();

			Controls.Add(closeButton);
		}
		private void DownloadButton_Click(object sender, EventArgs e)
		{
			// Specify the URL of the HTML file in the GitHub repository
			string githubHtmlUrl = "https://github.com/Khaled-Elagamy/EgyptTaxFileConverter/blob/main/template.html"; // Replace with the actual URL

			// Create a WebClient to download the file
			using (WebClient webClient = new WebClient())
			{
				try
				{
					// Set the filename for the downloaded file
					string filename = "template.html"; // Specify the desired filename

					// Download the HTML file and save it to a local path
					webClient.DownloadFile(githubHtmlUrl, filename);

					// Display a success message
					MessageBox.Show("HTML file downloaded successfully.", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Close();
				}
				catch (Exception ex)
				{
					// Handle any errors
					MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
