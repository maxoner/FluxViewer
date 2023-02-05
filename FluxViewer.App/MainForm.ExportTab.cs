using System.Media;
using FluxViewer.App.Enums;
using FluxViewer.DataAccess.Export;
using FluxViewer.DataAccess.Export.Exporters;
using FluxViewer.DataAccess.Storage;

namespace FluxViewer.App;

/// <summary>
/// UI-логика, связанная с вкладкой "ЭКСПОРТ"
/// </summary>
partial class MainForm
{
    // Изменили "Дата начала"
    private void beginExportDate_ValueChanged(object sender, EventArgs e)
    {
        CheckAndChangeDates();
        ExportDate_Value_Changed();
    }
    
    // Изменили "Дата конца"
    private void endExportDate_ValueChanged(object sender, EventArgs e)
    {
        CheckAndChangeDates();
        ExportDate_Value_Changed();
    }

    private void CheckAndChangeDates()
    {
        var beginDate = beginExportDate.Value.Date;
        var endDate = endExportDate.Value.Date;
        if (beginDate <= endDate)
            return;
        
        // Не даём пользователю начальную дату сделать большей, чем конечную
        SystemSounds.Beep.Play();
        beginExportDate.Value = endDate;
    }
    
    private void ExportDate_Value_Changed()
    {
        var beginDate = new DateTime(beginExportDate.Value.Year, beginExportDate.Value.Month, beginExportDate.Value.Day, 00, 00, 00, 000);
        var endDate = new DateTime(endExportDate.Value.Year, endExportDate.Value.Month, endExportDate.Value.Day, 23, 59, 59, 999);

        var dataCount = _storage.GetDataCountBetweenTwoDates(beginDate, endDate);
        exportButton.Enabled = (dataCount != 0);    // Деактивируем кнопку "Экспорт", если нечего экспортировать

        
        exportDataCountTextBox.Text = $"{dataCount} шт.";   // Выводим кол-во точек
        var allDatesWithData = _storage.GetAllDatesWithDataBetweenTwoDates(beginDate, endDate);
        if (allDatesWithData.Any())
        {
            firstExportDateTextBox.Text = allDatesWithData.First().ToString("d"); // Выводим первую фактическую дату 
            lastExportDateTextBox.Text = allDatesWithData.Last().ToString("d"); // Выводим последнюю фактическую дату
        }
        else
        {
            firstExportDateTextBox.Text = "";
            lastExportDateTextBox.Text = "";
        }
            
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

        var exporter = new Exporter(
            new DateTime(beginExportDate.Value.Year, beginExportDate.Value.Month, beginExportDate.Value.Day, 00, 00, 00, 000),
            new DateTime(endExportDate.Value.Year, endExportDate.Value.Month, endExportDate.Value.Day, 23, 59, 59, 999),
            _storage,
            ProvideFileExporterByExporterType(saveFileDialog.FileName),
            fillHolesCheckBox.Checked
        );
        
        exportProgressBar.Maximum = exporter.NumberOfExportIterations();
        exportProgressBar.Step = 1;

        foreach (var _ in exporter.Export())
        {
            exportProgressBar.PerformStep();
        }
        SystemSounds.Exclamation.Play();
        exportProgressBar.Value = 0;
    }

    private string SaveDialogFilterByExporterType()
    {
        var exporterString = exportTypeComboBox.SelectedItem.ToString();
        var exporterType = ExportTypeHelper.FromString(exporterString ?? string.Empty);
        return exporterType switch
        {
            ExportType.PlainText => "TXT|*.txt",
            ExportType.Csv => "CSV|*.csv",
            ExportType.Json => "JSON|*.json",
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

        var exporterString = exportTypeComboBox.SelectedItem.ToString();
        var exporterType = ExportTypeHelper.FromString(exporterString ?? string.Empty);
        return exporterType switch
        {
            ExportType.PlainText => new PlainTextFileExporter(pathToFile, dateTimeFormat, dateTimeNeedExport,
                fluxNeedExport, tempNeedExport, hummNeedExport, presTimeNeedExport),
            ExportType.Csv => new CsvFileExporter(pathToFile, dateTimeFormat, dateTimeNeedExport, fluxNeedExport,
                tempNeedExport, hummNeedExport, presTimeNeedExport),
            ExportType.Json => new JsonFileExporter(pathToFile, dateTimeFormat, dateTimeNeedExport, fluxNeedExport,
                tempNeedExport, hummNeedExport, presTimeNeedExport),
            _ => throw new Exception("Неизвестный тип экспортёра")
        };
    }

    private void dateTimeForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        dateFormatComboBox.Enabled = dateTimeForExportCheckBox.Checked;
    }
}