# W01 Assignment Notes

## 1. Pizza Seed Data (including additional pizza)

GET /pizza response:
```json
[{"id":1,"name":"Classic Italian","isGlutenFree":false},{"id":2,"name":"Veggie","isGlutenFree":true},{"id":3,"name":"Pepperoni Supreme","isGlutenFree":false}]
```

The additional pizza I added is **Pepperoni Supreme** (id 3).

## 2. CRUD Evidence

### GET all
- Request: `GET http://localhost:5087/pizza`
- Response: `[{"id":1,"name":"Classic Italian","isGlutenFree":false},{"id":2,"name":"Veggie","isGlutenFree":true},{"id":3,"name":"Pepperoni Supreme","isGlutenFree":false}]`
- Status: **200 OK**

### GET one
- Request: `GET http://localhost:5087/pizza/3`
- Response: `{"id":3,"name":"Pepperoni Supreme","isGlutenFree":false}`
- Status: **200 OK**

### POST
- Request: `POST http://localhost:5087/pizza`
- Body: `{"name":"Hawaiian","isGlutenFree":false}`
- Response: `{"id":4,"name":"Hawaiian","isGlutenFree":false}`
- Status: **201 Created**

### PUT
- Request: `PUT http://localhost:5087/pizza/4`
- Body: `{"id":4,"name":"Hawaiian Deluxe","isGlutenFree":false}`
- Response: (empty)
- Status: **204 No Content**

### DELETE
- Request: `DELETE http://localhost:5087/pizza/4`
- Response: (empty)
- Status: **204 No Content**

## 3. Sales Summary Function

```csharp
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
```

Sample output:
```
Sales Summary
----------------------------
 Total Sales: ₦1,076.50

 Details:
  store1.txt: ₦425.75
  store2.txt: ₦650.75
```