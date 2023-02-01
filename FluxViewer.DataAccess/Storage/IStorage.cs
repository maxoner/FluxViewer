using System;
using System.Collections.Generic;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Storage;

/// <summary>
/// Интерфейс взаимодействия с хранилищем показаний прибора.
/// В нём доступны методы чтения/записи показаний.
/// </summary>
public interface IStorage
{
    /// <summary>
    /// Открыть хранилище.
    /// </summary>
    void Open();

    /// <summary>
    /// Закрыть хранилище.
    /// </summary>
    void Close();

    /// <summary>
    /// Добавление показания прибора в хранилище.
    /// </summary>
    /// <param name="data">Структура, описывающая 1 показание прибора</param>
    void WriteData(NewData data);

    /// <summary>
    /// Получение количества всех показаний, записанных за всё время.
    /// </summary>
    /// <returns>Количество показаний прибора</returns>
    public int GetDataCount();

    /// <summary>
    /// Получение количества показаний между двумя датами.
    /// </summary>
    /// <param name="beginDate">Дата начала, с которой считаются показания</param>
    /// <param name="endDate">Дата конца, по которую считаются показания</param>
    /// <returns>Количество показаний прибора</returns>
    public int GetDataCountBetweenTwoDates(DateTime beginDate, DateTime endDate);

    /// <summary>
    /// Записывал ли прибор показания прибора в данную дату?
    /// </summary>
    /// <param name="date">Дата, за которую нужно проверить писал ли прибор показания</param>
    /// <returns>true - если есть данные за эту дату, иначе - false</returns>
    public bool HasDataForThisDate(DateTime date);

    /// <summary>
    /// Получение показаний прибора между за конкретную дату.
    /// </summary>
    /// <param name="date">Дата в которую следует искать показания</param>
    /// <returns>Все показания прибора, полученные в текущую дату</returns>
    public List<NewData> GetDataBatchByDate(DateTime date);

    /// <summary>
    /// Получить показания прибора за следующую дату после переданной.
    /// Например:
    ///     У нас есть показания за: 12.01.2022, 14.01.2022 и 17.01.2022.
    ///     GetNextBatchAfterThisDate(12.01.2022) вернёт данные за 14.01.2022.
    ///     GetNextBatchAfterThisDate(14.01.2022) вернёт данные за 17.01.2022.
    ///     GetNextBatchAfterThisDate(17.01.2022) вернёт ошибку!
    /// </summary>
    /// <param name="date">Дата, от которой ищется следующая дата, в которую прибор записывал показания</param>
    /// <returns>Показания прибора, если нашлась дата, позже переданной, иначе - ошибка</returns>
    public List<NewData> GetNextDataBatchAfterThisDate(DateTime date);

    /// <summary>
    /// Получить показания прибора за предыдущую дату после переданной.
    /// Например:
    ///     У нас есть показания за: 12.01.2022, 14.01.2022 и 17.01.2022.
    ///     GetPrevDataBatchAfterThisDate(12.01.2022) вернёт ошибку!
    ///     GetPrevDataBatchAfterThisDate(14.01.2022) вернёт данные за 12.01.2022.
    ///     GetPrevDataBatchAfterThisDate(17.01.2022) вернёт данные за 14.01.2022.
    /// </summary>
    /// <param name="date">Дата, от которой ищется предыдущая дата, в которую прибор записывал показания</param>
    /// <returns>Показания прибора, если нашлась дата, раньше переданной, иначе - ошибка</returns>
    public List<NewData> GetPrevDataBatchAfterThisDate(DateTime date);
}

public class NextDataBatchNotFoundException : Exception
{
}

public class PrevDataBatchNotFoundException : Exception
{
}