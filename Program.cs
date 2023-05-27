// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory,"stores");
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);
var salesFiles = FindFiles(storesDirectory);
var salesTotal = CalculateSalesTotal(salesFiles);

File.AppendAllText(Path.Combine(salesTotalDir,"totals.txt"), $"{salesTotal}{Environment.NewLine}");

foreach(var file in salesFiles)
{
    Console.WriteLine(file);
}

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
    foreach(var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if(extension == ".json")
        {
            salesFiles.Add(file);   
        }
    }
    return salesFiles;
}
double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotals = 0;
    
    foreach(string file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);    
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        salesTotals += data?.Total ?? 0;
    }

    return salesTotals;
}

record SalesData (double Total);


