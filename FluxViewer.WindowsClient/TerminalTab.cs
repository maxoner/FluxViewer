namespace FluxViewer.WindowsClient;

partial class MainForm
{
    /// <summary>
    /// Была выбрана вкладка "Терминал"
    /// </summary>
    private void terminalTabPage_Enter(object sender, EventArgs e)
    {
        if (_isDataStartFlux)
        {
            MessageBox.Show(
                "Для вывода данных в ASCII, необходимо сначала отключить запись показаний прибора!",
                "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            leftMenuTabPage.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Нажали кнопку "Запустить вывод данных в ASCII/Остановить вывод данных в ASCII"
    /// </summary>
    private void outputToAsciiButton_Click(object sender, EventArgs e)
    {
        if (outputToAsciiButton.Text == "Запустить вывод данных в ASCII")
        {
            outputToAsciiButton.Text = "Остановить вывод данных в ASCII";
            _isAsciiConsole = true;
            com_send_cmd(0xc1);
            if (!_isDataStartFlux)
                btn_start_Click(sender, e);
        }
        else
        {
            outputToAsciiButton.Text = "Запустить вывод данных в ASCII";
            com_send_cmd(0xc2);
            btn_stop_Click(sender, e);
            _isAsciiConsole = false;
        }
    }
}