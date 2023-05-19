namespace FluxViewer.App;

partial class MainForm
{
    /// <summary>
    /// Нажали кнопку "Запустить вывод данных в ASCII/Остановить вывод данных в ASCII"
    /// </summary>
    private void outputToAsciiButton_Click(object sender, EventArgs e)
    {
        if (_isDataStartFlux)
        {
            MessageBox.Show("Для начала необходимо отключить запись показаний прибора!", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
            
        }
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