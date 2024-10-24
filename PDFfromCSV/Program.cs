
using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using System.Formats.Asn1;
using CsvHelper;



namespace PDFfromCSV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define the input CSV file path and output PDF file path
            string csvPath = "Data.csv";
            string PdfPath = "SummaryReport.pdf";

        
            try
            {
                // Creating a PdfWriter instance to write to the output file
                PdfWriter pdfWriter = new PdfWriter(PdfPath);
                // Creating a PdfDocument instance
                PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                // Creating a Document object to add elements
                Document document = new Document(pdfDocument);

                // Adding Title to the PDF
                document.Add(new Paragraph("Summary Report")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetBold());

                // Create and populate the table
                var records = ReadCsvData(csvPath, out var headers);
                var table = new iText.Layout.Element.Table(headers.Length).SetWidth(UnitValue.CreatePercentValue(100)); // Set the table width to 100%

                // Add header cells with formatting
                foreach (var header in headers)
                {
                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph(header).SetBold())
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER)); // Setting background color for the header
                }

                // Add data rows
                foreach (var record in records)
                {
                    foreach (var field in record)
                    {
                        // Add each field from the record
                        table.AddCell(new Cell()
                                .Add(new Paragraph(field))
                                .SetTextAlignment(TextAlignment.CENTER));
                    }
                }

                // Set the table alignment to center
                table.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                document.Add(table); // Add the table to the document

                document.Close();
                pdfDocument.Close();
                pdfWriter.Close();

                Console.WriteLine("PDF generated successfully: " + PdfPath);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Directory not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.GetType()}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
        private static List<string[]> ReadCsvData(string path, out string[] headers)
        {
            var records = new List<string[]>();

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                // Read the header line manually
                string headerLine = reader.ReadLine();
                headers = headerLine?.Split(','); // Split by comma

                // Read the remaining records
                while (csv.Read())
                {
                    // Read each record as an array of strings
                    var record = new string[headers.Length]; 
                    // Use the length of headers
                    for (int i = 0; i < headers.Length; i++)
                    {
                        // Get field by index
                        record[i] = csv.GetField(i); 
                    }
                    records.Add(record);
                }
            }

            return records;
        }
    }
}