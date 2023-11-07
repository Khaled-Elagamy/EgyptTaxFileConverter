using Newtonsoft.Json;
using Nustache.Core;
using Squirrel;
using System.Dynamic;
using System.Reflection;
using System.Text;
namespace Converter
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		private async void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				using (var manager = await UpdateManager.GitHubUpdateManager(@"https://github.com/Khaled-Elagamy/EgyptTaxFileConverter", "Converter"))
				{
					// Disable update check when in develop mode.
					if (!manager.IsInstalledApp)
					{
						return;
					}
					var updateinfo = await manager.CheckForUpdate();
					if (updateinfo == null)
					{
						this.Text += " V. Latest Version";
					}
					else
					{
						this.Text += $" V. {manager.CurrentlyInstalledVersion()}";
						if (updateinfo.ReleasesToApply.Count > 1)
						{
							label1.Text = "New update Avaliable \nThe app will be Updated after restart";
							label1.Show();
							await manager.UpdateApp();
							//UpdateManager.RestartApp();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.Text += ex;
			}

		}
		private void browseButton_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*";
			//openFileDialog.Filter = "JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				inputPath.Text = openFileDialog.FileName;
				label1.Text = "The Pdf file will be created using the Template html";
				label1.ForeColor = SystemColors.ControlText;
				label1.BackColor = SystemColors.ActiveCaptionText;
				label1.Show();
				label2.Show();
			}

		}
		private void label2_Click(object sender, EventArgs e)
		{
			Get_html_file();
		}
		private void ConvertButton_Click(object sender, EventArgs e)
		{
			string directoryPath = Path.GetDirectoryName(openFileDialog.FileName);
			using (saveFileDialog)
			{
				saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
				saveFileDialog.InitialDirectory = directoryPath;

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					savePath.Text = saveFileDialog.FileName;
				}
				else { return; }
			}
			if (string.IsNullOrEmpty(openFileDialog.FileName) || string.IsNullOrEmpty(saveFileDialog.FileName))
			{
				MessageBox.Show("Please provide input Json and output PDF file paths.", "Error");
				return;
			}
			else
			{
				DeserializeJson(inputPath.Text);
			}
		}
		private string Get_html_data()
		{
			string template_path = Path.Combine(Directory.GetCurrentDirectory(), "template.html");
			string htmlContent;
			//check template auto
			if (template_html.FileName == "" && File.Exists(template_path))
			{
				template_html.FileName = template_path;
			}
			//ccheck template manual
			if (File.Exists(template_html.FileName))
			{
				label2.Text = "using File";
				htmlContent = File.ReadAllText(template_html.FileName);
				return htmlContent;
			}
			else
			{
				label2.Text = "using resources";
				var assembly = Assembly.GetExecutingAssembly();
				var resourceStream = assembly.GetManifestResourceStream("Converter.template.html");
				if (resourceStream != null)
				{
					using (var reader = new StreamReader(resourceStream))
					{
						htmlContent = reader.ReadToEnd();
						return htmlContent;
					}
				}
				else
				{
					return null;
				}
			}
		}
		private void Get_html_file()
		{
			using (template_html)
			{
				template_html.InitialDirectory = Directory.GetCurrentDirectory();
				template_html.Filter = "Html files (*.html)|*.html";
				//openFileDialog.Filter = "JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml";
				if (template_html.ShowDialog() == DialogResult.OK)
				{
					string selectedFileName = Path.GetFileName(template_html.FileName);
					label1.Text = "Creating Pdf File using " + selectedFileName;
					label2.Text = "Click here to change the file";
				}
			}
		}
		private void DeserializeJson(string jsonFilePath)
		{
			string jsonString = File.ReadAllText(jsonFilePath, Encoding.UTF8);
			InvoiceModel invoiceData = JsonConvert.DeserializeObject<InvoiceModel>(jsonString);
			DocumentModel document = JsonConvert.DeserializeObject<DocumentModel>(invoiceData.document);
			invoiceData.Documents = document;
			if (invoiceData != null && document != null)
			{
				htmltopdf(saveFileDialog.FileName, invoiceData, document);
			}
			else
			{
				MessageBox.Show("The File has Wrong Format.", "Error");
				return;
			}
		}

		private void htmltopdf(string outputPath, InvoiceModel invoice, DocumentModel document)
		{
			/*
            // JSON data for the invoice
            var invoiceData = new
            {
                status = invoice.status,
                dateTimeIssued = invoice.dateTimeIssued,
                dateTimeReceived = invoice.dateTimeReceived,
                issuerName = invoice.issuerName,
                uuid = invoice.uuid,
                taxpayerActivityCode = document.taxpayerActivityCode,
                issuerId = invoice.issuerId,
                postalCode = document.issuer.address.postalCode,
                street = document.issuer.address.street,
                branchID = document.issuer.address.branchID,
                country = document.issuer.address.country,
                regionCity = document.issuer.address.regionCity,
                floor = document.issuer.address.floor,
                CustomerName = document.receiver.name,
                room = document.issuer.address.room,
                buildingnumber = document.issuer.address.buildingNumber,

                // Add more data as needed
            };
            */
			/*Create new file with data formatting*/
			switch (invoice.status.ToLower())
			{
				case "submitted":
					invoice.status = "مُقَدَّم";
					break;
				case "valid":
					invoice.status = "صحيح";
					break;
				case "invalid":
					invoice.status = "غير صحيح";
					break;
				case "rejected":
					invoice.status = "مرفوض";
					break;
				case "cancelled":
					invoice.status = "ملغى";
					break;
				default:
					invoice.status = "غير محدد";
					break;
			}
			dynamic invoiceData = new ExpandoObject();
			invoiceData = invoice;
			dynamic documentData = new ExpandoObject();
			documentData = document;
			//Create DTO with the data needed and the inovice object to create json file and other stuff
			// Combine the data into a single dictionary
			var combinedData = new Dictionary<string, object>
			{
				{ "I", invoiceData },
				{ "D", documentData }
			};
			string filePath = "combinedData.txt"; // Specify the file path

			Type objType = invoiceData.GetType();
			var properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			using (StreamWriter writer = new StreamWriter(filePath))
			{
				foreach (var property in properties)
				{
					var value = property.GetValue(invoiceData);
					writer.WriteLine($"{property.Name}: {value}");
				}
			}
			try
			{
				string htmlTemplate = Get_html_data();
				if (htmlTemplate == null)
				{
					string message = "Error reading html file. Please Choose one manually\nYou Can Download it from here";
					string linkText = "Download";
					string linkUrl = "https://github.com/Khaled-Elagamy/EgyptTaxFileConverter/blob/main/Converter/template.html"; // Replace with the actual URL

					using (var customMessageBox = new CustomMessageBox(message, linkText, linkUrl))
					{
						customMessageBox.ShowDialog();
					}
					Get_html_file();
					htmlTemplate = File.ReadAllText(template_html.FileName);
				}
				//string htmlTemplate = File.ReadAllText(templatePath);
				string renderedHtml = Render.StringToString(htmlTemplate, combinedData);
				PdfGenerator.GeneratePdf(renderedHtml, outputPath);
				savePath.Clear();
				saveFileDialog.FileName = null;
				label1.Text = "File Created successfully";
				label1.ForeColor = Color.Black;
				label1.BackColor = Color.Green;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error reading the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}





		/*private void CreateJson()
        {
            var combinedData = new ExpandoObject() as IDictionary<string, object>;

            foreach (var property in invoice.GetType().GetProperties())
            {
                combinedData[property.Name] = property.GetValue(invoice);
            }

            dynamic documentData = new ExpandoObject();
            combinedData["document"] = documentData;

            // Add properties from DocumentModel to combinedData
            foreach (var property in document.GetType().GetProperties())
            {
                combinedData[property.Name] = property.GetValue(document);
            }



            Type objType = invoiceData.GetType();
            var properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(invoiceData);
                    writer.WriteLine($"{property.Name}: {value}");
                }
            }

            // Or, to write the data to a text file
            string filePath = "combinedData.txt"; // Specify the file path
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var kvp in combinedData)
                {
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
        }
        */
		/*
      try
      {
      CreatePdf(pdfFilePath, xmlFilePath);

      MessageBox.Show($"PDF successfully generated as {pdfFilePath}", "Success");
      }
      catch (Exception ex)
      {
      MessageBox.Show($"An error occurred: {ex.Message}", "Error");
      }*/



		/*
        private void CreatePdf(string pdfFilePath, string xmlFilePath)
        {
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfFilePath, FileMode.Create));

            doc.Open();

            // Create a base font for Arabic text (e.g., Arial for Arabic)
            BaseFont arabicBaseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            // Create a font for Arabic text
            iTextSharp.text.Font arabicFont = new iTextSharp.text.Font(arabicBaseFont, 12);

            // Load the XML data
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            // Extract data from the XML
            string invoiceNumber = xmlDoc.SelectSingleNode("/document/issuerId").InnerText;
            string date = xmlDoc.SelectSingleNode("/document/dateTimeIssued").InnerText;
            string customerName = xmlDoc.SelectSingleNode("/document/issuerName").InnerText;
            string total = xmlDoc.SelectSingleNode("/document/total").InnerText;

            // Create a paragraph for the PDF
            Paragraph paragraph = new Paragraph();
            paragraph.Alignment = Element.ALIGN_LEFT;

            // Add the data to the paragraph
            paragraph.Add(new Chunk($"Invoice Number: {invoiceNumber}\n", arabicFont));
            paragraph.Add(new Chunk($"Date: {date}\n", arabicFont));
            paragraph.Add(new Chunk($"Customer Name: {customerName}\n", arabicFont));
            paragraph.Add(new Chunk($"Total: {total}\n", arabicFont));

            doc.Add(paragraph);
            doc.Close();
              try
            {
                // Use Process.Start to open the File Explorer at the specified path
                Process.Start("explorer.exe", pdfFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
	}



}

