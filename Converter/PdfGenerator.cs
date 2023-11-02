using DinkToPdf;
using System.Diagnostics;

namespace Converter
{
    public class PdfGenerator
    {
        public static void GeneratePdf(string htmlContent, string outputPath)
        {
            SynchronizedConverter converter = PdfConverterSingleton.GetInstance();

            byte[] pdfBytes = converter.Convert(new HtmlToPdfDocument
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = DinkToPdf.Orientation.Portrait,
                PaperSize = new PechkinPaperSize("11.03in", "15.58in"),
                /*page size 11.03 × 15.58 in (portrait)*/
            },
                Objects = {
                new ObjectSettings
                {
                    HtmlContent = htmlContent ,
                    WebSettings={DefaultEncoding="UTF-8" },
                }
            }
            });
            // Save the PDF to a file
            File.WriteAllBytes(outputPath, pdfBytes);
            try
            {
                // Use Process.Start to open the File Explorer at the specified path
                Process.Start("explorer.exe", outputPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
