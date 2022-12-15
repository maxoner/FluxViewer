using FluxViewer.DataAccess.LiteDbb;
using FluxViewer.DataAccess.Models;

float lastpercentage = 0;
var currentDir = Directory.GetCurrentDirectory();
Console.WriteLine(currentDir);

var liteDbService = new LiteDbService();
liteDbService.ConnectOrCreateDataBase($"{currentDir}/testDb.db");

var iterationsCount = (int)Math.Pow(10, 9);
var testStartDate = DateTime.Now;
var insertValue = (int)Math.Pow(10, 5);

var data = new List<Data>(insertValue);

for (long i = 0; i < iterationsCount; i++)
{
    data.Add(new Data()
    {
        DateTime = testStartDate.AddMilliseconds(500),
        FluxSensorData = (float)i %20/100,
        HumiditySensorData = (float)i %100/100,
        PressureSensorData = (float)i %10 /100,
        TempSensorData = (float)i %50 /100
    });

    if (data.Count == insertValue)
    {
        PrintProgressPercentage(i, iterationsCount);
        Console.SetCursorPosition(0, 2);
        Console.WriteLine("Inserting in db.");
        liteDbService.WriteData(data);
        Console.SetCursorPosition(0, 2);
        Console.WriteLine("                 ");
        data.Clear();
        PrintProgressPercentage(i, iterationsCount);
    }
}

Console.WriteLine("Finished.");

Console.WriteLine();

void PrintProgressPercentage(long iterated, long length)
{
    float percentage = ((float)iterated / length)*100;
    
    if(percentage > 0.0001 &&  (percentage - lastpercentage) > 0.0001)
    {
        lastpercentage = percentage;
        Console.SetCursorPosition(0, 1);
        Console.WriteLine($"Completed {percentage.ToString("0.0000")}%.");
    }
}

