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
    void WriteData(Data data);


    /// <summary>
    /// "Пакетное" получение показаний прибора между двумя датами из хранилища.
    /// </summary>
    /// <param name="beginDate">Дата начала, с которой следует искать показания</param>
    /// <param name="endDate">Дата конца, по которую следует искать показания</param>
    /// <param name="batchNumber">Номер пакета</param>
    /// <param name="batchSize">Размер пакета</param>
    /// <returns>Все показания прибора в текущий временной интервал</returns>
    public List<Data> GetDataBatchBetweenTwoDates(DateTime beginDate, DateTime endDate, int batchNumber, int batchSize);
}