using DinkToPdf;

namespace Converter
{
    public class PdfConverterSingleton
    {
        private static SynchronizedConverter converterInstance;

        private PdfConverterSingleton()
        {
            // Private constructor to prevent external instantiation
        }

        public static SynchronizedConverter GetInstance()
        {
            if (converterInstance == null)
            {
                converterInstance = new SynchronizedConverter(new PdfTools());
            }
            return converterInstance;
        }
    }
}
