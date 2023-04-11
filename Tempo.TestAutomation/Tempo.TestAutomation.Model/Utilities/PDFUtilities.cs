using Datacom.TestAutomation.Common;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text;

namespace Tempo.TestAutomation.Model.Utilities
{
    public static class PDFUtilities
    {
        public static PdfDocument? PDFDocument;
        public static PdfReader? PDFReader;
        public static string GetPDFText(string path)
        {
            if (IsFilePDF(path))
            {
                PDFReader = new PdfReader(path);
                PDFDocument = new PdfDocument(PDFReader);
                StringBuilder pageContent = new StringBuilder();

                for (int page = 1; page <= PDFDocument.GetNumberOfPages(); page++)
                {
                    ITextExtractionStrategy textExtractionStrategy = new SimpleTextExtractionStrategy();
                    pageContent = pageContent.Append(PdfTextExtractor.GetTextFromPage(PDFDocument.GetPage(page), textExtractionStrategy));
                }

                PDFReader.Close();
                PDFDocument.Close();
                return pageContent.ToString();
            }
            else
                throw new Exception("File provided is not a PDF file type");
        }

        private static bool IsFilePDF(string path)
        {
            if (path.IsNotNullOrEmpty())
            {
                if (File.Exists(path))
                {
                    FileInfo latestFile = new FileInfo(path);
                    return latestFile.Extension.Equals(".pdf");
                }
                else
                    throw new Exception("File provided does not exist.");
            }
            else
                throw new Exception("No file path provided.");
        }
    }
}