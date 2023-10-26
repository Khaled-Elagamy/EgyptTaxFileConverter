using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
namespace Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.Filter = "JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml";
            if (filedialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = filedialog.FileName;
                string xmlFilePath = filedialog.FileName;

                // Extract the directory from the input XML file path
                string directoryPath = Path.GetDirectoryName(xmlFilePath);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                    saveFileDialog.InitialDirectory = directoryPath;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBox2.Text = saveFileDialog.FileName;
                    }

                }
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            string xmlFilePath = textBox1.Text;
            string pdfFilePath = textBox2.Text;
            if (string.IsNullOrEmpty(xmlFilePath) || string.IsNullOrEmpty(pdfFilePath))
            {
                MessageBox.Show("Please provide input XML and output PDF file paths.", "Error");
                return;
            }
            string jsonString = File.ReadAllText(xmlFilePath, Encoding.UTF8);
            // Deserialize the outer object to get the "document" property as a string
            //var outerObject = JsonConvert.DeserializeObject<OuterObject>(jsonString);

            // Parse the embedded JSON within the "document" property
            //var documentObject = JObject.Parse(outerObject.Document);

            // Now you can work with the embedded JSON object
            //var invoiceData = documentObject.ToObject<InvoiceModel>();

            var invoiceData = JsonConvert.DeserializeObject<InvoiceModel>(jsonString);
            DocumentModel? document = JsonConvert.DeserializeObject<DocumentModel>(invoiceData.document);

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

