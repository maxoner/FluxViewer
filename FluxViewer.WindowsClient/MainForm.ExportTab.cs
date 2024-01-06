using System.Media;
using FluxViewer.WindowsClient.Enums;
using FluxViewer.DataAccess.Controllers;
using FluxViewer.DataAccess.Export;

namespace FluxViewer.WindowsClient;

/// <summary>
/// UI-логика, связанная с вкладкой "ЭКСПОРТ"
/// </summary>
partial class MainForm
{
    private long _dataCount; // Кол-во показний прибора за выбранный промежуток дат

    private void InitExportTab()
    {
        InitExportTypeComboBox();
        InitDateFormatComboBox();

        UpdateExportInfo();
        ChangeExportButtonState();
    }

    private void InitExportTypeComboBox()
    {
        foreach (var exportType in Enum.GetValues<FileExporterType>())
        {
            eExportTypeComboBox.Items.Add(FileExporterTypeHelper.ToString(exportType));
        }

        eExportTypeComboBox.SelectedIndex = 0;
    }

    private void InitDateFormatComboBox()
    {
        foreach (var exportDateType in Enum.GetValues<ExportDateType>())
        {
            eDateFormatComboBox.Items.Add(ExportDateTypeHelper.ExampleByType(exportDateType));
        }

        eDateFormatComboBox.SelectedIndex = 0;
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

        foreach (var _ in exportController.Export(saveFileDialog.FileName))
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

        var numOfPoints = NumOfPointToHumanFormat(_dataCount);
        eExportDataCountTextBox.Text = (eFillHolesCheckBox.Checked) ? $"> {numOfPoints}" : $"~ {numOfPoints}";

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

        var approximatExportSize = BytesToHumanFormat(exportController.GetApproximateExportSizeInBytes());
        eApproximateExportSizeTextBox.Text =
            (eFillHolesCheckBox.Checked) ? $"> {approximatExportSize}" : $"~ {approximatExportSize}";
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
        else // Иначе декативируем её!
        {
            eExportButton.Enabled = false;
        }
    }

    private string SaveDialogFilterByExporterType()
    {
        var fileExporterType = ProvideFileExporterType();
        return fileExporterType switch
        {
            FileExporterType.PlainTextExporter => "TXT|*.txt",
            FileExporterType.CsvExporter => "CSV|*.csv",
            FileExporterType.JsonExporter => "JSON|*.json",
            _ => throw new Exception("Неизвестный тип экспортёра")
        };
    }

    private ExportController GetExportController()
    {
        var dateTimeExample = eDateFormatComboBox.SelectedItem.ToString();
        var dateTimeFormat = ExportDateTypeHelper.FromExample(dateTimeExample).ToString();
        var dateTimeNeedExport = dateTimeForExportCheckBox.Checked;
        var fluxNeedExport = fluxForExportCheckBox.Checked;
        var tempNeedExport = tempForExportCheckBox.Checked;
        var presTimeNeedExport = presForExportCheckBox.Checked;
        var hummNeedExport = hummForExportCheckBox.Checked;

        return new ExportController(
            new DateTime(eBeginExportDate.Value.Year, eBeginExportDate.Value.Month, eBeginExportDate.Value.Day, 00, 00,
                00, 000),
            new DateTime(eEndExportDate.Value.Year, eEndExportDate.Value.Month, eEndExportDate.Value.Day, 23, 59, 59,
                999),
            _storage,
            ProvideFileExporterType(),
            eFillHolesCheckBox.Checked,
            dateTimeFormat,
            dateTimeNeedExport,
            fluxNeedExport,
            tempNeedExport,
            presTimeNeedExport,
            hummNeedExport
        );
    }

    private FileExporterType ProvideFileExporterType()
    {
        var fileExporterString = eExportTypeComboBox.SelectedItem.ToString();
        return FileExporterTypeHelper.FromString(fileExporterString ?? string.Empty);
    }

    private static string NumOfPointToHumanFormat(long numOfPoints)
    {
        if (numOfPoints < 10000)
            return $"{numOfPoints}";
        var numOfThousand = (float)numOfPoints / 1000;
        if (numOfThousand < 1000)
            return $"{Math.Round(numOfThousand, 2)} тыс.";
        var numOfMillions = numOfThousand / 1000;
        if (numOfMillions < 1000)
            return $"{Math.Round(numOfMillions, 2)} млн.";
        var numOfBillion = numOfMillions / 1000;
        return $"{Math.Round(numOfBillion, 2)} млрд."; // Ну уж больше чем 999млрд. не будет же? :D
    }

    private static string BytesToHumanFormat(long numOfBytes)
    {
        if (numOfBytes < 1024)
            return $"{numOfBytes} байт";
        var numOfKb = (float)numOfBytes / 1024;
        if (numOfKb < 1024)
            return $"{Math.Round(numOfKb, 2)} Кб";
        var numOfMb = numOfKb / 1024;
        if (numOfMb < 1024)
            return $"{Math.Round(numOfMb, 2)} Мб";
        var numOfGb = numOfMb / 1024;
        if (numOfGb < 1024)
            return $"{Math.Round(numOfGb, 2)} Гб";
        var numOfTb = numOfGb / 1024; // До этого же никогда не дойдёт? Так ведь? :D
        return $"{Math.Round(numOfTb, 2)} Тб";
    }
}