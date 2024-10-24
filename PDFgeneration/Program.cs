
using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.StyledXmlParser.Jsoup.Nodes;
using System.Runtime.InteropServices;
using System.Linq.Expressions;

namespace PDFgeneration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string title = "Monthly Sales Report";
            string text = "Total Sales: $10,000";
            string imagePath = @"C:\Users\Administrator\sales.jpg";
            string pdfPath = @"SalesReport.pdf";
            CreatePdf(pdfPath, title, text, imagePath);
            Console.WriteLine($"PDF generated successfully at {pdfPath}");
        }
        
        static void CreatePdf(string pdfPath,string text,string imagePath,string title)
            
            {
            // Create a PDF writer
            using (PdfWriter writer = new PdfWriter(pdfPath))
            {
                //Create a PDF document
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    //Create a document layout
                    
                    Document document = new Document(pdf);
                    //Add the title
                    document.Add(new Paragraph(title)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetBold());
                    //Add new line paragraph
                    document.Add(new Paragraph("\n"));
                    //Add new text
                    document.Add(new Paragraph(text)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetFontSize(18));
                    //Add new line paragraph
                    document.Add(new Paragraph("\n"));
                    //Add image 
                    if (File.Exists(imagePath))
                    {

                        // Creating an Image instance from the image path
                        Image img = new Image(iText.IO.Image.ImageDataFactory.Create(imagePath));
                        img.SetAutoScale(true); // Scale the image to fit
                        img.SetTextAlignment(TextAlignment.CENTER); // Center the image
                        document.Add(img); // Adding the image to the document
                    }
                    else
                    {

                        document.Add(new Paragraph("Image not found!" + imagePath));
                        ;
                    }
                    //close the document
                    document.close();

                }
            }
            
        }
               
          
            }
        }
   
