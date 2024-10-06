namespace FluxViewer.WindowsClient;

partial class MainForm
{
    private void InitSettingsTab()
    {
        InitConnectTab();
        InitChannelTab();
    }

    /// <summary>
    /// Перешли на вкладку "Настройки"
    /// </summary>
    private void settingsTabPage_Enter(object sender, EventArgs e)
    {
        leftMenuTabPage.SelectedIndex = 0;  // Всегда показываем первую вкладку!
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