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
    /// Нажали на кнопку "Обновить список портов"
    /// </summary>
    private void updatePortsButton_Click(object sender, EventArgs e)
    {
        UpdateComPorts();
        UpdateConnectButtonState();
        SystemSounds.Beep.Play();
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