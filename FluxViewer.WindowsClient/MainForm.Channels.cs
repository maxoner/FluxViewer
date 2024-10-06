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

    private void RedrawChannels()
    {
        mainPanel.Controls.Clear(); // Удаляем все существующие каналы (и ниже рисуем их вновь)

        var channels = ChannelContextHolder.GetInstance().Channels;
        if (channels.Count == 0) // нечего рисовать --> выходим
            return;
        var windowsInfo = WindowDistributer.DistributeWindows(mainPanel.Width, mainPanel.Height, channels.Count);

        for (var i = 0; i < channels.Count; i++)
        {
            var channel = channels[i];
            var windowInfo = windowsInfo[i];

            // Создаем новый канал
            var channelForm = new Form
            {
                Name = $"channelForm{channel.Id}",
                TopLevel = false, // Убираем статус топ-уровневого окна, чтобы встроить форму
                FormBorderStyle = FormBorderStyle.FixedSingle, // Можно использовать любые другие стили границы
                Size = new Size(windowInfo.Width, windowInfo.Height),
                Location = new Point(windowInfo.X, windowInfo.Y),
                Text = channel.Name
            };

            mainPanel.Controls.Add(channelForm); // Добавляем канал в форму
            channelForm.Show(); // Делаем канал видимым
        }
    }
}