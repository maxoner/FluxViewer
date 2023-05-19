using FluxViewer.DataAccess.Storage;

void CreatePlausibleDataFiles(DateTime beginDate, DateTime endDate, float timeDeltaMilliseconds)
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
            var value = (double)(currentDate.Hour * 60 * 60 * 100 + currentDate.Minute * 60 * 100 +
                                 currentDate.Millisecond) / 1000000;
            file.Write(new Data(
                    currentDate,
                    (float)(Math.Sin(value) + Math.Cos(value)),
                    (float)(Math.Cos(value) - Math.Sin(value)),
                    (float)Math.Sin(value),
                    (float)Math.Cos(value)
                ).Serialize()
            );
            currentDate = currentDate.AddMilliseconds(timeDeltaMilliseconds);
        }
    }
}


var beginDate = new DateTime(2022, 11, 24);
var endDate = new DateTime(2022, 12, 1);
CreatePlausibleDataFiles(beginDate, endDate, 400);