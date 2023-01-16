using FluxViewer.DataAccess.Models;

void CreateFileWithRandomData(DateTime date, long dataCount)
{
    var pathToStorageDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
    if (!Directory.Exists(pathToStorageDir))
        Directory.CreateDirectory(pathToStorageDir);
    
    var filename = date.ToString("yyyy_MM_dd") + ".flux";
    var pathToFile = Path.Combine(pathToStorageDir, filename);
    using var streamWriter = new StreamWriter(pathToFile);
    for (var i = 0; i < dataCount; i++)
    {
        streamWriter.WriteLine(new Data()
        {
            DateTime = DateTime.Now,
            FluxSensorData = (float)DateTime.Now.Millisecond % 20 / 100,
            HumiditySensorData = (float)DateTime.Now.Millisecond % 100 / 100,
            PressureSensorData = (float)DateTime.Now.Millisecond % 10 / 100,
            TempSensorData = (float)DateTime.Now.Millisecond % 50 / 100
        });
    }
    streamWriter.WriteLine(dataCount);
}


var beginDate = new DateTime(2022, 11, 24);
var endDate = new DateTime(2023, 01, 15);
const int dataCount = 216000;

var currentDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day);
while (currentDate < endDate)
{
    CreateFileWithRandomData(currentDate, dataCount);
    currentDate = currentDate.AddDays(1);
}