using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Nustache.Core;
using System.Diagnostics;
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

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputPath.Text = openFileDialog.FileName;
            }
        }
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            // Extract the directory from the input XML file path
            string directoryPath = Path.GetDirectoryName(openFileDialog.FileName);
            using (saveFileDialog)
            {
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog.InitialDirectory = directoryPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    savePath.Text = saveFileDialog.FileName;
                }
            }
            if (string.IsNullOrEmpty(openFileDialog.FileName) || string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                MessageBox.Show("Please provide input XML and output PDF file paths.", "Error");
                return;
            }
            else
            {
                DeserializeJson(inputPath.Text);
            }
            // Deserialize the outer object to get the "document" property as a string
            //var outerObject = JsonConvert.DeserializeObject<OuterObject>(jsonString);

            // Parse the embedded JSON within the "document" property
            //var documentObject = JObject.Parse(outerObject.Document);

            // Now you can work with the embedded JSON object
            //var invoiceData = documentObject.ToObject<InvoiceModel>();
        }
        private void DeserializeJson(string jsonFilePath)
        {
            string jsonString = File.ReadAllText(jsonFilePath, Encoding.UTF8);
            InvoiceModel invoiceData = JsonConvert.DeserializeObject<InvoiceModel>(jsonString);
            DocumentModel? document = JsonConvert.DeserializeObject<DocumentModel>(invoiceData.document);
            invoiceData.Documents = document;
            if (invoiceData != null && document != null)
            {
                //CreatePdf(invoiceData, document, saveFileDialog.FileName);
                string template = "F:\\code learning\\projects\\Converter\\Converter\\template.html";
                //FillPdfTemplate(template, saveFileDialog.FileName, invoiceData.uuid, document?.issuer.name);
                htmltopdf(template, saveFileDialog.FileName, invoiceData, document);
            }
            else
            {
                MessageBox.Show("The File has Wrong Format.", "Error");
                return;
            }
        }

        private void htmltopdf(string templatePath, string outputPath, InvoiceModel invoice, DocumentModel document)
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



            // Load your HTML template with placeholders
            string htmlTemplate = File.ReadAllText(templatePath);
            string renderedHtml = Render.StringToString(htmlTemplate, combinedData);

            // Replace placeholders in the HTML template with actual data
            /*
                htmlTemplate = htmlTemplate.Replace("{{InvoiceNumber}}", invoiceData.InvoiceNumber);
                htmlTemplate = htmlTemplate.Replace("{{CustomerName}}", invoiceData.CustomerName);
                htmlTemplate = htmlTemplate.Replace("{{CustomerName}}", invoiceData.CustomerName);
                htmlTemplate = htmlTemplate.Replace("{{CustomerName}}", invoiceData.CustomerName);
                htmlTemplate = htmlTemplate.Replace("{{CustomerName}}", invoiceData.CustomerName);
            */
            // Add more replacements as needed

            // Create a PDF document from the HTML content
            PdfGenerator.GeneratePdf(renderedHtml, outputPath);
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
        private void CreatePdf(InvoiceModel invoiceData, DocumentModel document, string pdfFilePath)
        {
            using (Document pdfDocument = new Document())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDocument, new FileStream(pdfFilePath, FileMode.Create));
                pdfDocument.Open();

                //BaseFont baseFont = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                // Create the font for Arabic text
                iTextSharp.text.Font arabicFont = GetArial();

                // Create a new paragraph to add text
                Paragraph paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_LEFT;

                // Add text to the paragraph
                paragraph.Add(new Chunk("Invoice Data:" + invoiceData.dateTimeIssued, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD)));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk("Invoice Number: " + invoiceData.uuid, GetArial()));

                // Add the paragraph to the document
                pdfDocument.Add(paragraph);

                ColumnText ct = new ColumnText(writer.DirectContent);
                ct.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // Set text direction to RTL
                ct.SetSimpleColumn(100, 200, 400, 750); // Adjust the coordinates as needed

                // Create a Phrase with Arabic text
                Phrase arabicPhrase = new Phrase(new Chunk("Customer Name: " + document?.issuer.name, arabicFont));

                // Add the Phrase to the ColumnText
                ct.AddText(arabicPhrase);

                // Go to add the text to the document
                ct.Go();

                // Close the document
                pdfDocument.Close();
                MessageBox.Show($"PDF successfully generated as {pdfFilePath}", "Success");
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
        }
        public static iTextSharp.text.Font GetArial()
        {
            string fontName = "arial";
            if (!FontFactory.IsRegistered(fontName))
            {
                var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf";
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                FontFactory.Register(fontPath, fontName);
            }
            //new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD)
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10);
        }

        public void FillPdfTemplate(string templatePath, string outputPath, string invoiceNumber, string customerName)
        {
            using (var pdfReader = new PdfReader(templatePath))
            using (var pdfStream = new FileStream(outputPath, FileMode.Create))
            using (var pdfStamper = new PdfStamper(pdfReader, pdfStream))
            {
                BaseFont baseFont = BaseFont.CreateFont(":\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                var pdfFormFields = pdfStamper.AcroFields;
                pdfFormFields.SetFieldProperty("InvoiceNumber", "textfont", baseFont, null);
                pdfFormFields.SetFieldProperty("CustomerName", "textfont", baseFont, null);
                // Replace placeholders in the PDF template with actual data
                pdfFormFields.SetField("InvoiceNumber", invoiceNumber);
                pdfFormFields.SetField("customerName", customerName);

                // Flatten the PDF so the data is no longer editable
                pdfStamper.FormFlattening = true;
            }
        }
        public void fillablePdf()
        {
            // Create a new document
            using (Document doc = new Document())
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("fillable_form.pdf", FileMode.Create));
                doc.Open();

                // Create a text field
                TextField textField = new TextField(writer, new iTextSharp.text.Rectangle(100, 500, 200, 520), "text_field");
                textField.Alignment = Element.ALIGN_LEFT;
                textField.FontSize = 12f;
                PdfFormField textFormField = textField.GetTextField();
                textFormField.SetFieldFlags(PdfFormField.FF_MULTILINE);
                writer.AddAnnotation(textFormField);

                // Close the document
                doc.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fillablePdf();
        }



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

