using FluxViewer.Core.ChannelContext;
using FluxViewer.Core.Controllers;
using FluxViewer.Core.GraphThemes;
using ZedGraph;

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
        channelsMainPanel.Controls.Clear(); // Удаляем все существующие каналы (и ниже рисуем их вновь)

        var channelContextHolder = ChannelContextHolder.GetInstance();
        var displayedChannels =
            channelContextHolder.Channels.Where(channel => channel.Display).ToList(); // каналы, которые надо отобразить

        if (displayedChannels.Count == 0) // нечего рисовать --> выходим
            return;

        var channelCount = displayedChannels.Count + 1; // +1 , т.к. рисуем ещё "Общий канал"
        var windowsInfo = WindowDistributer.DistributeWindows(channelsMainPanel.Width, channelsMainPanel.Height, channelCount);

        for (var i = 0; i < displayedChannels.Count; i++)
        {
            var channel = displayedChannels[i];
            // Создаем канал
            var windowInfo = windowsInfo[i];
            var channelForm = CreateChannelZedGraphControl(windowInfo, channel.Name, channel.Units);
            channelForm.Show(); // Делаем канал видимым
            channelsMainPanel.Controls.Add(channelForm);
        }

        // Создаем "Общий канал"
        var commonWindowInfo = windowsInfo.Last();
        var commonChannelForm = CreateChannelZedGraphControl(commonWindowInfo, "Общий канал", "");
        commonChannelForm.Show(); // Делаем канал видимым
        channelsMainPanel.Controls.Add(commonChannelForm);
    }

    /// <summary>
    /// Создаёт окно (где будут распологаться графики) использую библиотеку zedGraph
    /// </summary>
    /// <param name="name">Уникальное имя окна</param>
    /// <param name="windowInfo">Размеры и координаты окна</param>
    /// <param name="title">Заголовок окна</param>
    /// <returns></returns>
    private static ZedGraphControl CreateChannelZedGraphControl(WindowInfo windowInfo, string title, string units)
    {
        var graphControl = new ZedGraphControl
        {
            Size = new Size(windowInfo.Width, windowInfo.Height),
            Location = new Point(windowInfo.X, windowInfo.Y),
        };
        var graphPanel = graphControl.GraphPane;
        var graphController = new GraphController(graphPanel);
        graphController.SetGraphTitle(title);
        graphController.SetXAxisTitle("Время, с");
        graphController.SetYAxisTitle($"{title}, {units}");
        graphController.ShowGrid();
        graphController.SetGraphTheme(new BlackGraphTheme());
        return graphControl;
    }
}