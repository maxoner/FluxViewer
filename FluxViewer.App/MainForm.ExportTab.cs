using System.Media;
using FluxViewer.App.Enums;
using FluxViewer.DataAccess.Controllers;
using FluxViewer.DataAccess.Export.Exporters;

namespace FluxViewer.App;

/// <summary>
/// UI-логика, связанная с вкладкой "ЭКСПОРТ"
/// </summary>
partial class MainForm
{   
    // Фокус на вкладке "ЭКСПОРТ"
    private void exportTabPage_Enter(object sender, EventArgs e)
    {
        UpdateExportInfo();
    }
    
    // Изменили "Дата начала"
    private void beginExportDate_ValueChanged(object sender, EventArgs e)
    {
        CheckAndChangeDates();
        UpdateExportInfo();
    }
    
    // Изменили "Дата конца"
    private void endExportDate_ValueChanged(object sender, EventArgs e)
    {
        CheckAndChangeDates();
        UpdateExportInfo();
    }
    
    // Нажали на флажок "Дата и время"
    private void dateTimeForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        dateFormatComboBox.Enabled = dateTimeForExportCheckBox.Checked;
        CheckAllCheckboxes();
    }
    
    // Нажали на флажок "Электростатическое поле"
    private void fluxForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckAllCheckboxes();
    }
    
    // Нажали на флажок "Температура"
    private void tempForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckAllCheckboxes();
    }

    // Нажали на флажок "Давление"
    private void presForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckAllCheckboxes();
    }
    
    // Нажали на флажок "Влажность"
    private void hummForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckAllCheckboxes();
    }
    
    // Нажали кнопку "Экспорт"
    private void exportButton_Click(object sender, EventArgs e)
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = SaveDialogFilterByExporterType();
        saveFileDialog.RestoreDirectory = true;
        saveFileDialog.CreatePrompt = true;
        saveFileDialog.Title = "Сохранить выбраный отрезок как";
        if (saveFileDialog.ShowDialog() != DialogResult.OK)
            return;
        
        var exportController = GetExportController();
        exportProgressBar.Maximum = exportController.NumberOfExportIterations();
        exportProgressBar.Step = 1;
        
        var fileExporter = ProvideFileExporterByExporterType(saveFileDialog.FileName);
        foreach (var _ in exportController.Export(fileExporter))
        {
            exportProgressBar.PerformStep();
        }
        SystemSounds.Exclamation.Play();
        exportProgressBar.Value = 0;
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
    
    private void UpdateExportInfo()
    {
        var exportController = GetExportController();
        var dataCount = exportController.GetDataCount();
        exportButton.Enabled = (dataCount != 0);    // Деактивируем кнопку "Экспорт", если нечего экспортировать
        
        exportDataCountTextBox.Text = $"{dataCount} шт.";   // Выводим кол-во точек
        var allDatesWithData = exportController.GetAllDatesWithData();
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

    private void CheckAllCheckboxes()
    {
        // Пока хотя бы 1 флажок активен - кнопка "Экспорт" активна, иначе - неактивна.
        exportButton.Enabled = dateTimeForExportCheckBox.Checked || 
                               fluxForExportCheckBox.Checked ||
                               tempForExportCheckBox.Checked ||
                               hummForExportCheckBox.Checked || 
                               presForExportCheckBox.Checked;
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
        var dateTimeExample = dateFormatComboBox.SelectedItem.ToString();
        var dateTimeFormat = ExportDateTypeHelper.FromExample(dateTimeExample).ToString();
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

    private ExportController GetExportController()
    {
        return new ExportController(
            new DateTime(beginExportDate.Value.Year, beginExportDate.Value.Month, beginExportDate.Value.Day, 00, 00, 00, 000),
            new DateTime(endExportDate.Value.Year, endExportDate.Value.Month, endExportDate.Value.Day, 23, 59, 59, 999),
            _storage,
            fillHolesCheckBox.Checked
        );
    }
}