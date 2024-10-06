namespace FluxViewer.WindowsClient;

using Core.ChannelContext;

partial class MainForm
{
    private void InitChannelTab()
    {
        RedrawChannelSettingDataGridView();
    }

    /// <summary>
    /// Была выбрана вкладка "Виртуальные каналы"
    /// </summary>
    private void channelsSettingsTabPage_Enter(object sender, EventArgs e)
    {
        RedrawChannelSettingDataGridView();
    }

    /// <summary>
    /// Обработка нажатия кнопки "Добавить"
    /// </summary>
    private void addChannelButton_Click(object sender, EventArgs e)
    {
        var channelContextHolder = ChannelContextHolder.GetInstance();
        var channels = channelContextHolder.Channels;
        var newChannelId = (channels.Count == 0) ? 0 : channels.Last().Id + 1;
        channelContextHolder.AddChannel(new BasicChannel
        {
            Id = newChannelId,
            Name = $"Канал{newChannelId}",
            Units = "усл. ед.",
            Display = true,
            Save = true,
            XValue = 1,
            FreeValue = 0,
            PhysicalChannel = 1,
        });
        RedrawChannelSettingDataGridView();
    }

    /// <summary>
    /// Обработка нажатия кнопки "Удалить"
    /// </summary>
    private void removeChannelButton_Click(object sender, EventArgs e)
    {
        var channelContextHolder = ChannelContextHolder.GetInstance();

        // Бежим по всем выделенным строкам (может быть больше одной) и удаляем выделенные каналы из контекста
        foreach (DataGridViewRow selectedRow in channelSettingDataGridView.SelectedRows)
        {
            var channelId =
                Convert.ToInt32(selectedRow.Cells[0]!.Value); // Получаем индекс канала из первой (!!!) ячейки 
            channelContextHolder.RemoveChannelById(channelId); // удаляем из контекста
        }

        RedrawChannelSettingDataGridView();
    }


    /// <summary>
    /// Обработка событий по выделению ячеек/строк в таблице `channelSettingDataGridView`
    /// </summary>
    private void channelSettingDataGridView_SelectionChanged(object sender, EventArgs e)
    {
        // Если нет выделенных строк, то и кнопку "Удалить" не делаем активной
        removeChannelButton.Enabled = channelSettingDataGridView.SelectedRows.Count > 0;
    }

    /// <summary>
    /// Обработка события отображения элемента управления для редактирования ячейки в таблице `channelSettingDataGridView`
    /// </summary>
    private void channelSettingDataGridView_EditingControlShowing(object sender,
        DataGridViewEditingControlShowingEventArgs e)
    {
        // Добавляем на столбцы "X коэф.", "Свободный коэф." и "Физический канал" доп. обработчик ввода только дробных чисел
        if (channelSettingDataGridView.CurrentCell.ColumnIndex is 4 or 5 or 6)
        {
            var textBox = e.Control as TextBox;
            if (textBox != null)
                textBox.KeyPress += TextBoxWithIntegerContext_KeyPress;
        }
    }

    /// <summary>
    /// // Проверяем, является ли нажатая клавиша цифрой или клавишей Backspace
    /// </summary>
    private void TextBoxWithIntegerContext_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true; // Отменяем ввод
        }
    }

    /// <summary>
    /// Обработка событий по изменению любой ячейки в таблице `channelSettingDataGridView` 
    /// </summary>
    private void channelSettingDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        var channelContextHolder = ChannelContextHolder.GetInstance();
        var channels = channelContextHolder.Channels;

        var row = channelSettingDataGridView.Rows[e.RowIndex];
        var channelId = Convert.ToInt32(row.Cells[0]!.Value); // Получаем индекс канала из первой (!!!) ячейки
        var channel = channels.FirstOrDefault(c => c.Id == channelId)!; // Находим канал по его ID

        // В зависимости от индекса ячейки, изменяем соответствующее поле
        switch (e.ColumnIndex)
        {
            case 1: // Обновляем "Имя"
                channel.Name = row.Cells[1]!.Value.ToString();
                break;
            case 2: // Обновляем "Ед. изм."
                channel.Units = row.Cells[2]!.Value.ToString();
                break;
            case 3: // Обновляем "Тип"
                // TODO: допиши как появится поддержка перемножающего и суммирующего типов
                break;
            case 4: // Обновляем "X коэф."
                switch (channel)
                {
                    // Обработка Базового канала
                    case BasicChannel basicChannel:
                        basicChannel.XValue = Convert.ToDouble(row.Cells[4]!.Value);
                        break;
                    // Обработка Перемножающего канала
                    case MultiplicativeChannel:
                        // TODO: допиши, как появится поддержка перемножающего канала
                        break;
                    // Обработка Суммирующего канала
                    case SummationChannel:
                        // TODO: допиши, как появится поддержка суммирующего канала
                        break;
                }

                break;
            case 5: // Обновляем "Свободный коэф."
                switch (channel)
                {
                    // Обработка Базового канала
                    case BasicChannel basicChannel:
                        basicChannel.FreeValue = Convert.ToDouble(row.Cells[5]!.Value);
                        break;
                    // Обработка Перемножающего канала
                    case MultiplicativeChannel:
                        // TODO: допиши, как появится поддержка перемножающего канала
                        break;
                    // Обработка Суммирующего канала
                    case SummationChannel:
                        // TODO: допиши, как появится поддержка суммирующего канала
                        break;
                }

                break;
            case 6: // Обновляем "Физический канал"
                switch (channel)
                {
                    // Обработка Базового канала
                    case BasicChannel basicChannel:
                        basicChannel.PhysicalChannel = Convert.ToInt32(row.Cells[6]!.Value);
                        break;
                    // Обработка Перемножающего канала
                    case MultiplicativeChannel:
                        // TODO: допиши, как появится поддержка перемножающего канала
                        break;
                    // Обработка Суммирующего канала
                    case SummationChannel:
                        // TODO: допиши, как появится поддержка суммирующего канала
                        break;
                }

                break;
            case 7: // Обновляем "Отображение"
                channel.Display = Convert.ToBoolean(row.Cells[7]!.Value);
                break;
            case 8: // Обновляем "Отображать ли в общем канале"
                var displayInCommonChannel = Convert.ToBoolean(row.Cells[8]!.Value);
                if (displayInCommonChannel)
                    channelContextHolder.AddChannelFromGeneralWindowById(channelId);
                else
                    channelContextHolder.RemoveChannelFromGeneralWindowById(channelId);
                break;
            case 9:
                channel.Save = Convert.ToBoolean(row.Cells[9]!.Value); // Обновляем "Сохранение"
                break;
            default:
                throw new Exception("Невозможно обработать ячейку с данным индексом");
        }
    }

    /// <summary>
    /// Перерисовывает таблицу `channelSettingDataGridView` с настройками каналов
    /// </summary>
    private void RedrawChannelSettingDataGridView()
    {
        channelSettingDataGridView.Rows.Clear(); // Удаляем все строки (и ниже их заново рисуем)

        var channelContextHolder = ChannelContextHolder.GetInstance();
        var channels = channelContextHolder.Channels;
        var generalWindowChannelIds = channelContextHolder.GeneralWindowChannelIds;
        foreach (var channel in channels)
        {
            var displayInCommonChannel = generalWindowChannelIds.Contains(channel.Id); // Отображать в общем канале? 
            switch (channel)
            {
                case BasicChannel basicChannel:
                    channelSettingDataGridView.Rows.Add(
                        basicChannel.Id.ToString(), // Столбец "ID"
                        basicChannel.Name, // Столбец "Название"
                        basicChannel.Units, // Столбец "Ед. изм."
                        basicChannel.Type, // Столбец "Тип"
                        basicChannel.XValue, // Столбец "X коэф."
                        basicChannel.FreeValue, // Столбец "Свободный коэф."
                        basicChannel.PhysicalChannel, // Столбец "Физический канал"
                        basicChannel.Display.ToString(), // Столбец "Отображение"
                        displayInCommonChannel, // Столбец "Отображать в общем канале?" 
                        basicChannel.Save.ToString() // Столбец "Сохранение"
                    );
                    break;
                // Обработка Перемножающего канала
                case MultiplicativeChannel:
                    // TODO: допиши, как появится поддержка перемножающего канала
                    break;
                // Обработка Суммирующего канала
                case SummationChannel:
                    // TODO: допиши, как появится поддержка суммирующего канала
                    break;
            }
        }
    }
}