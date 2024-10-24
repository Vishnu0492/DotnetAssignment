using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
namespace MileStone_6_Part_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define file paths for CSV input and PDF output
            string csvFilePath = @"C:\Users\Administrator\source\repos\day21\PDFfromCSV\bin\Debug\net6.0\Data.csv";
            string pdfFilePath = @"SummaryReport.pdf";

            try
            {
                // Read and parse the CSV file
                var csvData = ReadCsvFile(csvFilePath);

                // Generate PDF report based on the CSV data
                GeneratePdfReport(csvData, pdfFilePath);

                Console.WriteLine("PDF report generated successfully at: " + pdfFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Function to read and parse CSV file
        static List<Tuple<string, string>> ReadCsvFile(string csvFilePath)
        {
            var csvData = new List<Tuple<string, string>>();

            // Read the CSV file line by line
            using (var reader = new StreamReader(csvFilePath))
            {
                string line;
                // Skip the header line
                reader.ReadLine();

                // Read each subsequent line
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    // Ensure correct parsing with two values per line
                    if (values.Length == 2)
                    {
                        csvData.Add(new Tuple<string, string>(values[0], values[1]));
                    }
                }
            }

            return csvData;
        }

        // Function to generate PDF report
        static void GeneratePdfReport(List<Tuple<string, string>> csvData, string pdfFilePath)
        {
            // Initialize the PDF document
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
            document.Open();

            // Add a title to the PDF
            Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            Paragraph title = new Paragraph("Sales Summary Report", titleFont)
            {
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(title);

            // Add a line break
            document.Add(new Paragraph("\n"));

            // Create a table with 2 columns (Name, Amount)
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 1, 1 });

            // Add header row with custom formatting
            AddCellToTable(table, "Name", FontFactory.GetFont("Arial", 12, Font.BOLD), BaseColor.LIGHT_GRAY);
            AddCellToTable(table, "Amount", FontFactory.GetFont("Arial", 12, Font.BOLD), BaseColor.LIGHT_GRAY);

            // Add data rows from the CSV to the table
            foreach (var row in csvData)
            {
                AddCellToTable(table, row.Item1, FontFactory.GetFont("Arial", 12, Font.NORMAL), BaseColor.WHITE);
                AddCellToTable(table, row.Item2, FontFactory.GetFont("Arial", 12, Font.NORMAL), BaseColor.WHITE);
            }

            // Add the table to the document
            document.Add(table);

            // Close the document
            document.Close();
        }

        // Helper function to add a cell to the PDF table
        static void AddCellToTable(PdfPTable table, string text, Font font, BaseColor backgroundColor)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = backgroundColor,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);
        }
    }
}