using FluxViewer.DataAccess.Exporters;
using FluxViewer.DataAccess.Models;
using FluxViewer.DataAccess.Storage;

void CreateDataFiles(DateTime beginDate, DateTime endDate, float timeDeltaMilliseconds)
{
    var random = new Random();
    var pathToStorageDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
    if (!Directory.Exists(pathToStorageDir))
        Directory.CreateDirectory(pathToStorageDir);

    for (var dayDelta = 0; dayDelta < (endDate - beginDate).Days; dayDelta++)
    {
        var currentDate = beginDate.AddDays(dayDelta);
        var filename = currentDate.ToString("yyyy_MM_dd") + ".flux";
        var pathToFile = Path.Combine(pathToStorageDir, filename);
        using var file = new FileStream(pathToFile, FileMode.Append);

        var nextDate = currentDate.AddDays(1);
        while (currentDate < nextDate)
        {
            file.Write(new NewData(
                    currentDate,
                    (float)(random.NextInt64(1, 30) * random.NextDouble()),
                    (float)(random.NextInt64(1, 30) * random.NextDouble()),
                    (float)(random.NextInt64(1, 30) * random.NextDouble()),
                    (float)(random.NextInt64(1, 30) * random.NextDouble())
                ).Serialize()
            );
            currentDate = currentDate.AddMilliseconds(timeDeltaMilliseconds);
        }
    }
}


var beginDate = new DateTime(2022, 11, 24);
var endDate = new DateTime(2023, 01, 15);
CreateDataFiles(beginDate, endDate, 400);
// var storage = new FileSystemStorage();
// storage.Open();
// Console.WriteLine(storage.GetDataCountBetweenTwoDates(new DateTime(2022, 11, 24), new DateTime(2022, 11, 24)));
// storage.Close();