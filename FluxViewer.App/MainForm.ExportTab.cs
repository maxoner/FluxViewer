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
    private int _dataCount;     // Кол-во показний прибора за выбранный промежуток дат
    

    // Фокус на вкладке "ЭКСПОРТ"
    private void exportTabPage_Enter(object sender, EventArgs e)
    {
        UpdateExportInfo();
        ChangeExportButtonState();
    }
    
    // Изменили "Дата начала"
    private void beginExportDate_ValueChanged(object sender, EventArgs e)
    {
        CheckAndChangeDatesInExportTab();
        UpdateExportInfo();
        ChangeExportButtonState();
    }
    
    // Изменили "Дата конца"
    private void endExportDate_ValueChanged(object sender, EventArgs e)
    {
        CheckAndChangeDatesInExportTab();
        UpdateExportInfo();
        ChangeExportButtonState();
    }
    
    // Нажали на флажок "Заполнять пробелы средним"
    private void fillHolesCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateExportInfo();
    }
    
    // Нажали на флажок "Дата и время"
    private void dateTimeForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        eDateFormatComboBox.Enabled = dateTimeForExportCheckBox.Checked;
        ChangeExportButtonState();
    }
    
    // Нажали на флажок "Электростатическое поле"
    private void fluxForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        ChangeExportButtonState();
    }
    
    // Нажали на флажок "Температура"
    private void tempForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        ChangeExportButtonState();
    }

    // Нажали на флажок "Давление"
    private void presForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        ChangeExportButtonState();
    }
    
    // Нажали на флажок "Влажность"
    private void hummForExportCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        ChangeExportButtonState();
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
        eExportProgressBar.Maximum = exportController.NumberOfExportIterations();
        eExportProgressBar.Step = 1;
        
        var fileExporter = ProvideFileExporterByExporterType(saveFileDialog.FileName);
        foreach (var _ in exportController.Export(fileExporter))
        {
            eExportProgressBar.PerformStep();
        }
        SystemSounds.Exclamation.Play();
        eExportProgressBar.Value = 0;
    }
    
    private void CheckAndChangeDatesInExportTab()
    {
        var beginDate = eBeginExportDate.Value.Date;
        var endDate = eEndExportDate.Value.Date;
        if (beginDate <= endDate)
            return;
        
        // Не даём пользователю начальную дату сделать большей, чем конечную
        SystemSounds.Beep.Play();
        eBeginExportDate.Value = endDate;
    }
    
    private void UpdateExportInfo()
    {
        var exportController = GetExportController();
        _dataCount = exportController.GetDataCount();

        if (eFillHolesCheckBox.Checked)
            eExportDataCountTextBox.Text = $"> {_dataCount} шт.";   // Выводим кол-во точек
        else
            eExportDataCountTextBox.Text = $"{_dataCount} шт.";   // Выводим кол-во точек
        
        var allDatesWithData = exportController.GetAllDatesWithData();
        if (allDatesWithData.Any())
        {
            eFirstExportDateTextBox.Text = allDatesWithData.First().ToString("d"); // Выводим первую фактическую дату 
            eLastExportDateTextBox.Text = allDatesWithData.Last().ToString("d"); // Выводим последнюю фактическую дату
        }
        else
        {
            eFirstExportDateTextBox.Text = "";
            eLastExportDateTextBox.Text = "";
        }
    }

    private void ChangeExportButtonState()
    {
        // Если есть что экспортировать и выбран хотя бы 1 параметр экспорта, то активируем кнопку экспорта
        if (_dataCount > 0 &&
            (dateTimeForExportCheckBox.Checked ||
             fluxForExportCheckBox.Checked ||
             tempForExportCheckBox.Checked ||
             hummForExportCheckBox.Checked ||
             presForExportCheckBox.Checked))
        {
            eExportButton.Enabled = true;
        }
        else    // Иначе декативируем её!
        {
            eExportButton.Enabled = false;
        }
    }

    private string SaveDialogFilterByExporterType()
    {
        var exporterString = eExportTypeComboBox.SelectedItem.ToString();
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
        var dateTimeExample = eDateFormatComboBox.SelectedItem.ToString();
        var dateTimeFormat = ExportDateTypeHelper.FromExample(dateTimeExample).ToString();
        var dateTimeNeedExport = dateTimeForExportCheckBox.Checked;
        var fluxNeedExport = fluxForExportCheckBox.Checked;
        var tempNeedExport = tempForExportCheckBox.Checked;
        var hummNeedExport = hummForExportCheckBox.Checked;
        var presTimeNeedExport = presForExportCheckBox.Checked;

        var exporterString = eExportTypeComboBox.SelectedItem.ToString();
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
            new DateTime(eBeginExportDate.Value.Year, eBeginExportDate.Value.Month, eBeginExportDate.Value.Day, 00, 00, 00, 000),
            new DateTime(eEndExportDate.Value.Year, eEndExportDate.Value.Month, eEndExportDate.Value.Day, 23, 59, 59, 999),
            _storage,
            eFillHolesCheckBox.Checked
        );
    }
}