namespace FluxViewer.WindowsClient;

/// <summary>
/// UI-логика, связанная с вкладкой "КАНАЛЫ"
/// </summary>
partial class MainForm
{
    // Счетчик для отслеживания количества добавленных форм
    int formCount = 0;

    private void addChannelButton_Click(object sender, EventArgs e)
    {
        // Создаем новую форму ChannelForm
        Form channelForm = new Form
        {
            Name = "form" + formCount,   // Даем уникальное имя
            TopLevel = false,            // Убираем статус топ-уровневого окна, чтобы встроить форму
            FormBorderStyle = FormBorderStyle.Sizable, // Можно использовать любые другие стили границы
            Size = new Size(300, 200),   // Размер встроенной формы
            Location = new Point(10, formCount * 210), // Позиция в panel2
            Text = "Канал " + (formCount + 1) // Заголовок формы "Канал 1", "Канал 2" и т.д.
        };

        // Добавляем форму в panel2
        panel2.Controls.Add(channelForm);

        // Отображаем форму
        channelForm.Show();

        formCount++;  // Увеличиваем счетчик добавленных форм
    }

    private void removeChannelButton_Click(object sender, EventArgs e)
    {
        if (formCount > 0)
        {
            // Находим последнюю добавленную форму
            Control lastForm = panel2.Controls["form" + (formCount - 1)];

            // Удаляем последнюю форму
            panel2.Controls.Remove(lastForm);

            formCount--;  // Уменьшаем счетчик
        }
    }
}