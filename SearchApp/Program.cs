// Jag har implementerat
// Singleton,
// Factory Method,
// Strategy,
// Observer.

// Det finns en kommentar exemplevis 
// public Action OnSomething; // Observer
// Vid varje ställe där desing mönstret är relevant.

// AsyncSearcher implementerar observer

// ISort, IPrint och dess implementationer är del av stratagy pattern
// och Printer använder ISort och IPrint

// ConsolePrint är en singleton

// ISearch definerar Factory Method 'CreateSearcher'
using System.Runtime.Serialization.Formatters.Binary;

const string SEARCH_FILE_ENDING = ".search.config";
const string PRINTER_FILE_ENDING = ".printer.config";

BinaryFormatter formatter = new();
ISearch search = default;
Printer printer = new();

if (args.Length == 1 || args.Length == 3)
{
    Directory.SetCurrentDirectory(args[args.Length == 1 ? 0 : 2]);
}

if(args.Length == 2 || args.Length == 3)
{
    LoadSearchConfig(args[0]);
    LoadPrintConfig(args[1]);
}
else
{
    Setup();
}

Console.WriteLine($"Searching from '{Directory.GetCurrentDirectory()}'");
var result = await search.SearchAsync();
printer.Print(result.ToList());
Console.WriteLine("Search complete.");
Console.WriteLine("Press any key to continue...");
Console.ReadKey();


Console.Clear();
Console.WriteLine("Do you wish to save current search parameters? (y/n)");
char c = Console.ReadKey().KeyChar;
if (c == 'y')
    SaveSearchConfig();
Console.Clear();
Console.WriteLine("Do you wish to save current print parameters? (y/n)");
c = Console.ReadKey().KeyChar;
if (c == 'y')
    SavePrintConfig();


void BuildSearchConfig()
{
    Console.Clear();
    Console.WriteLine("Search type options");
    Console.WriteLine("1. Current and all sub directory files");
    Console.WriteLine("2. Current directory files");
    while (search == default)
    {
        switch (Console.ReadKey().KeyChar - '0')
        {
            case 1:
                search = FileSearch.FromUser();
                break;
            case 2:
                search = CurrentDirectorySearch.FromUser();
                break;
        }
    }
    Console.Clear();
}

void BuildPrintConfig()
{
    Console.Clear();
    Console.WriteLine("Print type options");
    Console.WriteLine("1. File");
    Console.WriteLine("2. Console");
    while (printer.PrintStrategy == default)
    {
        switch (Console.ReadKey().KeyChar - '0')
        {
            case 1:
                printer.PrintStrategy = new TextFilePrint();
                break;
            case 2:
                printer.PrintStrategy = ConsolePrint.Instance;
                break;
        }
    }
    Console.Clear();
    Console.WriteLine("Print sorting options");
    Console.WriteLine("1. No sort");
    Console.WriteLine("2. Date");
    while (printer.OrderStrategy == default)
    {
        switch (Console.ReadKey().KeyChar - '0')
        {
            case 1:
                printer.OrderStrategy = new DontCareSort();
                break;
            case 2:
                printer.OrderStrategy = new FileDateSort();
                break;
        }
    }
    Console.Clear();
}

void LoadSearchConfig(string path)
{
    using var stream = File.OpenRead(path);
    search = formatter.Deserialize(stream) as ISearch;
}

void LoadPrintConfig(string path)
{
    using var stream = File.OpenRead(path);
    printer = formatter.Deserialize(stream) as Printer;
}

void Setup()
{
    var searchConfigs = Directory.GetFiles(Directory.GetCurrentDirectory()).Where(s => s.EndsWith(SEARCH_FILE_ENDING));

    Console.WriteLine("Select search configuration?");
    int k = 0;
    foreach (var config in searchConfigs)
        Console.WriteLine($"{(char)('0' + k++)}. {config.Split('\\').Last()}");
    if (searchConfigs.Count() == 0)
        Console.WriteLine("No avalible configurations!");
    Console.WriteLine("Create new press any other key");
    var searchPick = Console.ReadKey().KeyChar - '0';
    if (searchPick >= searchConfigs.Count())
        BuildSearchConfig();
    else
        LoadSearchConfig(searchConfigs.ElementAt(searchPick));


    var printConfigs = Directory.GetFiles(Directory.GetCurrentDirectory()).Where(s => s.EndsWith(PRINTER_FILE_ENDING));
    Console.WriteLine("Select print configuration?");
    k = 0;
    foreach (var config in printConfigs)
        Console.WriteLine($"{(char)('0' + k++)}. {config.Split('\\').Last()}");
    if (printConfigs.Count() == 0)
        Console.WriteLine("No avalible configurations!");
    Console.WriteLine("Create new press any other key");
    var printPick = Console.ReadKey().KeyChar - '0';
    if (printPick >= printConfigs.Count())
        BuildPrintConfig();
    else
        LoadPrintConfig(printConfigs.ElementAt(printPick));
}

void SaveSearchConfig()
{
    Console.Clear();
    Console.WriteLine("Save search config as... ");
    string savePath = Console.ReadLine() ?? "UNKNOWN_SEARCH_CONFIG";
    if (!savePath.EndsWith(SEARCH_FILE_ENDING))
        savePath += SEARCH_FILE_ENDING;
    using var saveSearch = File.OpenWrite(savePath);
    formatter.Serialize(saveSearch, search);
    saveSearch.Flush();
}

void SavePrintConfig()
{
    Console.Clear();
    Console.WriteLine("Save print config as... ");
    string savePath = Console.ReadLine() ?? "UNKNOWN_PRINT_CONFIG";
    if (!savePath.EndsWith(PRINTER_FILE_ENDING))
        savePath += PRINTER_FILE_ENDING;
    using var savePrint = File.OpenWrite(savePath);
    formatter.Serialize(savePrint, printer);
    savePrint.Flush();
}