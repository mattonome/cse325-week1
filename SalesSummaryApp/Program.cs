using System.Globalization;
using System.Text;

// Create sample sales data files for testing
string dataDir = "./salesdata";
Directory.CreateDirectory(dataDir);

File.WriteAllText(Path.Combine(dataDir, "store1.txt"), 
    "Widget, 100.50\nGadget, 250.00\nGizmo, 75.25");
File.WriteAllText(Path.Combine(dataDir, "store2.txt"), 
    "Widget, 200.00\nGadget, 150.75\nGizmo, 300.00");

// Generate the sales summary
GenerateSalesSummary(dataDir, "./sales-summary.txt");

Console.WriteLine("Sales summary written to ./sales-summary.txt");

static void GenerateSalesSummary(string directory, string outputFile)
{
    string[] files = Directory.GetFiles(directory, "*.txt");
    decimal grandTotal = 0m;
    var details = new StringBuilder();

    foreach (string file in files)
    {
        decimal fileTotal = 0m;
        foreach (string line in File.ReadAllLines(file))
        {
            string[] parts = line.Split(',');
            if (parts.Length > 0 &&
                decimal.TryParse(parts[^1].Trim(),
                    NumberStyles.Currency,
                    CultureInfo.CurrentCulture,
                    out decimal amount))
            {
                fileTotal += amount;
            }
        }
        grandTotal += fileTotal;
        details.AppendLine($"  {Path.GetFileName(file)}: {fileTotal:C}");
    }

    var report = new StringBuilder();
    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($" Total Sales: {grandTotal:C}");
    report.AppendLine();
    report.AppendLine(" Details:");
    report.Append(details);

    File.WriteAllText(outputFile, report.ToString());
    Console.WriteLine(report.ToString());
}