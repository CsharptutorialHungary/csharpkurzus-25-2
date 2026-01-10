using System.Text.RegularExpressions;

namespace RentalApp.Core.Tests;

internal class RentalAppTests
{

    private IRentalApp _app;
    private string _basePath;
    private string _equipmentDBFile;
    private string _customerDBFile;
    private string _reservationDBFile;


    [OneTimeSetUp]
    public void InitRentalApp()
    {

        _basePath = AppContext.BaseDirectory;

        _equipmentDBFile = Path.Combine(_basePath, "equipmentDB.json");
        _customerDBFile = Path.Combine(_basePath, "customerDB.json");
        _reservationDBFile = Path.Combine(_basePath, "reservationDB.json");

        _app = RentalAppFactory.Create(
            _equipmentDBFile,
            _customerDBFile,
            _reservationDBFile
            );

    }

    [SetUp]
    public void ClearDBFiles()
    {
        File.WriteAllText(_equipmentDBFile, "");
        File.WriteAllText(_customerDBFile, "");
        File.WriteAllText(_reservationDBFile, "");
    }


    private string RemoveWhiteSpaces(string input)
    {
        return Regex.Replace(input, @"\s", "");
    }

    private string GetTestInput(string jsonFilePath) => File.ReadAllText(Path.Combine(_basePath, jsonFilePath));

    [Test]
    public void AddEquipment_AllAttribsOk_AddsEquipment()
    {

        string input = GetTestInput("test_eq_all_attribs_ok1.json");

        string expected = RemoveWhiteSpaces("[" + input + "]");
        Console.WriteLine($"Expected:{Environment.NewLine}{expected}");

        _app.AddEquipment(input);
        string result = RemoveWhiteSpaces(File.ReadAllText(_equipmentDBFile));

        Assert.That(result, Is.EqualTo(expected));

    }
}
