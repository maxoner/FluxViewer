using FluxViewer.Core.ChannelContext;
using FluxViewer.Core.Controllers;

namespace FluxViewer.WindowsClient;

/// <summary>
/// UI-логика, связанная с вкладкой "КАНАЛЫ"
/// </summary>
partial class MainForm
{
    private void InitChannelsTab()
    {
        RedrawChannels();
    }

    /// <summary>
    /// Была выбрана вкладка "Каналы"
    /// </summary>
    private void channelsTabPage_Enter(object sender, EventArgs e)
    {
        RedrawChannels();
    }

    private void RedrawChannels()
    {
        mainPanel.Controls.Clear(); // Удаляем все существующие каналы (и ниже рисуем их вновь)

        var channelContextHolder = ChannelContextHolder.GetInstance();
        var displayedChannels =
            channelContextHolder.Channels.Where(channel => channel.Display).ToList(); // каналы, которые надо отобразить

        if (displayedChannels.Count == 0) // нечего рисовать --> выходим
            return;

        var channelCount = displayedChannels.Count + 1; // +1 , т.к. рисуем ещё "Общий канал"
        var windowsInfo = WindowDistributer.DistributeWindows(mainPanel.Width, mainPanel.Height, channelCount);

        for (var i = 0; i < displayedChannels.Count; i++)
        {
            var channel = displayedChannels[i];
            // Создаем канал
            var windowInfo = windowsInfo[i];
            var channelForm = CreateChannelForm($"channelForm{channel.Id}", windowInfo, channel.Name);
            channelForm.Show(); // Делаем канал видимым
            mainPanel.Controls.Add(channelForm);
        }

        // Создаем "Общий канал"
        var commonWindowInfo = windowsInfo.Last();
        var commonChannelForm = CreateChannelForm("commonChannel", commonWindowInfo, "Общий канал");
        commonChannelForm.Show(); // Делаем канал видимым
        mainPanel.Controls.Add(commonChannelForm);
    }

    private Form CreateChannelForm(string name, WindowInfo windowInfo, string text)
    {
        return new Form
        {
            Name = name,
            TopLevel = false, // Убираем статус топ-уровневого окна, чтобы встроить форму
            FormBorderStyle = FormBorderStyle.FixedToolWindow, // Можно использовать любые другие стили границы
            Size = new Size(windowInfo.Width, windowInfo.Height),
            Location = new Point(windowInfo.X, windowInfo.Y),
            ControlBox = false, // Убираем кнопки управления окном,
            Text = text
        };
    }
}