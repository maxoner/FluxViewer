using System.IO.Ports;
using System.Media;

namespace FluxViewer.App;

partial class MainForm
{
    private void InitConnectTab()
    {
        UpdateComPorts();
        UpdateConnectButtonState();
    }

    /// <summary>
    /// Была выбрана вкладка "Подключение"
    /// </summary>
    private void connectTabPage_Enter(object sender, EventArgs e)
    {
        UpdateComPorts();
        UpdateConnectButtonState();
    }

    /// <summary>
    /// Нажали на кнопку "Обновить список портов"
    /// </summary>
    private void updatePortsButton_Click(object sender, EventArgs e)
    {
        UpdateComPorts();
        UpdateConnectButtonState();
        SystemSounds.Beep.Play();
    }

    /// <summary>
    /// Нажали кнопку "Подключиться"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>								
    private void connectButton_Click(object sender, EventArgs e)
    {
        if (SerialPort.IsOpen)
        {
            SerialPort.Close();
        }

        _isTestButton = true;
        timer1.Interval = 1000;
        timer1.Enabled = true;
        connect_flux();
    }

    private void UpdateComPorts()
    {
        comNameComboBox.Items.Clear();
        var comPorts = SerialPort.GetPortNames();

        if (comPorts.Length == 0)
        {
            comNameComboBox.Text = "Нет портов";
            comNameComboBox.Enabled = false;
            return;
        }

        foreach (var comPort in comPorts)
        {
            comNameComboBox.Items.Add(comPort);
        }

        comNameComboBox.Enabled = true;
        comNameComboBox.SelectedIndex = 0;
    }

    private void UpdateConnectButtonState()
    {
        connectButton.Enabled = comNameComboBox.Items.Count != 0;
    }
}