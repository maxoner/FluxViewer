using FluxViewer.DataAccess.Exporters;
using FluxViewer.DataAccess.Storage;

namespace FluxViewer;

/// <summary>
/// UI-логика, связанная с вкладкой "ЭКСПОРТ"
/// </summary>
partial class Form1
{
    private void beginExportDate_ValueChanged(object sender, EventArgs e)
    {
        var beginDate = new DateTime(beginExportDate.Value.Year, beginExportDate.Value.Month, beginExportDate.Value.Day,
            00, 00, 00, 000);
        var endDate = new DateTime(endExportDate.Value.Year, endExportDate.Value.Month, endExportDate.Value.Day, 23, 59,
            59, 999);

        var dataCount = _storage.GetDataCountBetweenTwoDates(beginDate, endDate);
        exportDataCountTextBox.Text = $"{dataCount} шт.";
        exportButton.Enabled = (dataCount != 0);

        var allDatesWithData = _storage.GetAllDatesWithDataBetweenTwoDates(beginDate, endDate);
        firstExportDateTextBox.Text = allDatesWithData.First().ToString("d");
        lastExportDateTextBox.Text = allDatesWithData.Last().ToString("d");
    }

    private void exportButton_Click(object sender, EventArgs e)
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = SaveDialogFilterByExporterType();
        saveFileDialog.RestoreDirectory = true;
        saveFileDialog.CreatePrompt = true;
        saveFileDialog.Title = "Сохранить выбраный отрезок как";
        if (saveFileDialog.ShowDialog() != DialogResult.OK)
            return;

        var exporter = ProvideFileExporterByExporterType(saveFileDialog.FileName);

        var beginDate = new DateTime(beginExportDate.Value.Year, beginExportDate.Value.Month, beginExportDate.Value.Day, 00, 00, 00, 000);
        var endDate = new DateTime(endExportDate.Value.Year, endExportDate.Value.Month, endExportDate.Value.Day, 23, 59, 59, 999);
        var allDatesWithData = _storage.GetAllDatesWithDataBetweenTwoDates(beginDate, endDate);

        var fillHoles = fillHolesCheckBox.Checked;
        exportProgressBar.Maximum =
            fillHoles ? (allDatesWithData.Last() - allDatesWithData.First()).Days : allDatesWithData.Count();
        exportProgressBar.Step = 1;

        var currentDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 00, 00, 00);
        while (currentDate <= endDate)
        {
            if (_storage.HasDataForThisDate(currentDate))
            {
                var dataBatch = _storage.GetDataBatchByDate(currentDate);
                exporter.Export(fillHoles ? PlaceHolder.FillHoles(dataBatch) : dataBatch);
            }
            else
            {
                // Если надо заполнять пробелы и мы находимся в диапазоне дат, в которые прибор записывал
                // (чтобы иметь гарантии всегда получить предыдущий и следующий батч данных), то можем генерировать батч
                if (fillHoles && currentDate >= allDatesWithData.First() && currentDate <= allDatesWithData.Last())
                {
                    try
                    {
                        var prevDataBatch = _storage.GetPrevDataBatchAfterThisDate(currentDate);
                        var nextDataBatch = _storage.GetNextDataBatchAfterThisDate(currentDate);
                        var dataBatch = DataBatchGenerator.GenerateDataBatch(
                            currentDate,
                            250, // TODO: высчитывать на основе двух батчей
                            (prevDataBatch.Last().FluxSensorData + nextDataBatch.First().FluxSensorData) / 2,
                            (prevDataBatch.Last().TempSensorData + nextDataBatch.First().TempSensorData) / 2,
                            (prevDataBatch.Last().PressureSensorData + nextDataBatch.First().PressureSensorData) / 2,
                            (prevDataBatch.Last().HumiditySensorData + nextDataBatch.First().HumiditySensorData) / 2
                        );
                        exporter.Export(fillHoles ? PlaceHolder.FillHoles(dataBatch) : dataBatch);
                    }
                    catch (PrevDataBatchNotFoundException exception)
                    {
                        // TODO: в логи, т.к. странная ситуация    
                    }
                    catch (NextDataBatchNotFoundException exception)
                    {
                        // TODO: в логи, т.к. странная ситуация    
                    }
                }
            }

            currentDate = currentDate.AddDays(1);
            exportProgressBar.PerformStep();
        }

        exportProgressBar.Value = 0;
    }

    private string SaveDialogFilterByExporterType()
    {
        var exporterType = convertorComboBox.SelectedItem.ToString();
        return exporterType switch
        {
            "Plain Text" => "TXT|*.txt",
            "CSV" => "CSV|*.csv",
            "JSON" => "JSON|*.json",
            _ => throw new Exception("Неизвестный тип экспортёра")
        };
    }

    private FileExporter ProvideFileExporterByExporterType(string pathToFile)
    {
        var dateTimeFormat = dateFormatComboBox.SelectedItem.ToString();
        var dateTimeNeedExport = dateTimeForExportCheckBox.Checked;
        var fluxNeedExport = fluxForExportCheckBox.Checked;
        var tempNeedExport = tempForExportCheckBox.Checked;
        var hummNeedExport = hummForExportCheckBox.Checked;
        var presTimeNeedExport = presForExportCheckBox.Checked;

        var exporterType = convertorComboBox.SelectedItem.ToString();
        return exporterType switch
        {
            "Plain Text" => new PlainTextFileExporter(pathToFile, dateTimeFormat, dateTimeNeedExport,
                fluxNeedExport, tempNeedExport, hummNeedExport, presTimeNeedExport),
            "CSV" => new CsvFileExporter(pathToFile, dateTimeFormat, dateTimeNeedExport, fluxNeedExport,
                tempNeedExport, hummNeedExport, presTimeNeedExport),
            "JSON" => new JsonFileExporter(pathToFile, dateTimeFormat, dateTimeNeedExport, fluxNeedExport,
                tempNeedExport, hummNeedExport, presTimeNeedExport),
            _ => throw new Exception("Неизвестный тип экспортёра")
        };
    }

    private void dateTimeForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        dateFormatComboBox.Enabled = dateTimeForExportCheckBox.Checked;
    }
}