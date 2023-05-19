namespace FluxViewer.App;

partial class MainForm
{
    private void InitSettingsTab()
    {
        InitConnectTab();
    }


    /// <summary>
    /// Изменили вкладку в блоке "Настройки"
    /// </summary>
    private void leftMenuTabPage_Selecting(object sender, TabControlCancelEventArgs e)
    {
        if (e.TabPage == connectTabPage) // Выбрана вкладка "Подключение"
        {
            ConnectTabHasBeenSelected();
        }
        else if (e.TabPage == deviceSettignsTabPage) // Выбрана вкладка "Настройки устройства"
            if (SerialPort.IsOpen)
            {
                if (_isDataStartFlux)   // TODO: зарефактори это
                {
                    MessageBox.Show(
                        "Для просмотра и изменения настроек, необходимо сначала отключить запись показаний прибора!");
                }
                else
                {
                    com_send_cmd(0x1b); // Настройки устройства    
                }
            }
            else
            {
                gb_settings.Enabled = false;
            }
        else if (e.TabPageIndex == 2)
        {
        }
        else if (e.TabPageIndex == 3)
        {
        }
        else if (e.TabPageIndex == 4)
        {
        }
    }

    /// <summary>
    /// Стили табов настроек
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void leftMenuTabPage_DrawItem(object sender, DrawItemEventArgs e)
    {
        Graphics g = e.Graphics;
        Brush _textBrush;

        // Get the item from the collection.
        TabPage _tabPage = leftMenuTabPage.TabPages[e.Index];

        // Get the real bounds for the tab rectangle.
        Rectangle _tabBounds = leftMenuTabPage.GetTabRect(e.Index);

        if (e.State == DrawItemState.Selected)
        {
            // Draw a different background color, and don't paint a focus rectangle.
            _textBrush = new SolidBrush(Color.Yellow);
            g.FillRectangle(Brushes.Gray, e.Bounds);
        }
        else
        {
            _textBrush = new SolidBrush(e.ForeColor);
            e.DrawBackground();
        }

        // Use our own font.
        Font _tabFont = new Font("Arial", (float)14.0, FontStyle.Bold, GraphicsUnit.Pixel);

        // Draw string. Center the text.
        StringFormat _stringFlags = new StringFormat();
        _stringFlags.Alignment = StringAlignment.Center;
        _stringFlags.LineAlignment = StringAlignment.Center;
        g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
    }
}