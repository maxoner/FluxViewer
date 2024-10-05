using FluxViewer.Core.Controllers;

namespace FluxViewer.WindowsClient;

/// <summary>
/// UI-логика, связанная с вкладкой "КАНАЛЫ"
/// </summary>
partial class MainForm
{
    int channelCount; // Счетчик для отслеживания количества добавленных форм

    private void addChannelButton_Click(object sender, EventArgs e)
    {
        channelCount++;
        drawChannels();
    }

    private void removeChannelButton_Click(object sender, EventArgs e)
    {
        if (channelCount > 0)
            channelCount--;
        drawChannels();
    }

    private void drawChannels()
    {
        mainPanel.Controls.Clear(); // Удаляем все существующие формы (и ниже рисуем их вновь)

        if (channelCount == 0) // нечего рисовать --> выходим
            return;

        var windowsInfo = WindowDistributer.DistributeWindows(mainPanel.Width, mainPanel.Height, channelCount);
        foreach (var windowInfo in windowsInfo)
        {
            // Создаем новую форму ChannelForm
            var channelForm = new Form
            {
                Name = "form" + channelCount, // Даем уникальное имя
                TopLevel = false, // Убираем статус топ-уровневого окна, чтобы встроить форму
                FormBorderStyle = FormBorderStyle.Sizable, // Можно использовать любые другие стили границы
                Size = new Size(windowInfo.Width, windowInfo.Height), // Размер встроенной формы
                Location = new Point(windowInfo.X, windowInfo.Y), // Позиция в panel2
                Text = "Канал " + (channelCount + 1) // Заголовок формы "Канал 1", "Канал 2" и т.д.
            };
            // Добавляем форму в panel2
            mainPanel.Controls.Add(channelForm);

            // Отображаем форму
            channelForm.Show();
        }
    }
}