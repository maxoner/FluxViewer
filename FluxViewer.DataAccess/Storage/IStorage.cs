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
    public int GetDataCountBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null);  // TODO: выпилить step

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
}