namespace FluxViewer.WindowsClient;

partial class MainForm
{
    /// <summary>
    /// Была выбрана вкладка "Настройки устройства"
    /// </summary>
    private void deviceSettingsTabPage_Enter(object sender, EventArgs e)
    {
        if (!SerialPort.IsOpen)
        {
            gb_settings.Enabled = false;
            return;
        }


        if (_isDataStartFlux) // TODO: зарефактори это
        {
            MessageBox.Show(
                "Для просмотра и изменения настроек, необходимо сначала отключить запись показаний прибора!",
                "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            leftMenuTabPage.SelectedIndex = 0;
        }
        else
        {
            com_send_cmd(0x1b); // Настройки устройства    
        }
    }
}