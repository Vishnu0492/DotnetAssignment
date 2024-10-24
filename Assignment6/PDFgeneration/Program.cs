
using iText.IO.Image;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace PDFgeneration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string title = "Monthly Sales Report";
            string text = "Total Sales: $10,000";
            string imagePath = @"C:\Users\Administrator\sales.jpeg";
            string pdfPath = @"SalesReport.pdf";
            
            Console.WriteLine($"PDF generated successfully at {pdfPath}");


            try
            {
                // Create a new PDF document
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
                doc.Open();

                // Add the title
                Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                Paragraph titleParagraph = new Paragraph(title, titleFont);
                titleParagraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(titleParagraph);

                // Add a blank space after the title
                doc.Add(new Paragraph("\n"));

                // Add the text
                Font textFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
                Paragraph textParagraph = new Paragraph(text, textFont);
                textParagraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(textParagraph);

                // Add the image
                if (File.Exists(imagePath))
                {
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                    image.Alignment = Element.ALIGN_CENTER;
                    doc.Add(image);
                }
                else
                {
                    Console.WriteLine("Image file not found: " + imagePath);
                }

                // Close the document
                doc.Close();

                Console.WriteLine("PDF generated successfully at " + pdfPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);

            }
        }
    }
}
   
