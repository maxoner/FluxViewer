using System;
using System.Collections.Generic;
using FluxViewer.DataAccess.Storage;

namespace FluxViewer.DataAccess.Controllers;

/// <summary>
/// Класс с бизнес-логикой, отвечающей за отображение архивных данных прибора. 
/// </summary>
public class DataArchiveController
{
    public DateTime beginDate { get; }
    public DateTime endDate { get; }
    private readonly IStorage _storage;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="beginDate">Дата начала, с которой начинается экспорт архивных показаний прибора</param>
    /// <param name="endDate">Дата конца, по которую происходит экспорт архивных показаний прибора</param>
    /// <param name="storage">Хранилище, из которого происходит экспорт</param>
    public DataArchiveController(DateTime beginDate, DateTime endDate, IStorage storage)
    {
        this.beginDate = beginDate;
        this.endDate = endDate;
        _storage = storage;
    }

    /// <summary>
    /// Есть ли показания прибора в данном диапазоне дат?
    /// </summary>
    /// <returns>true, если есть показания в данном диапазоне, иначе - false</returns>
    public bool HasDataBetweenTwoDates()
    {
        return _storage.GetDataCountBetweenTwoDates(beginDate, endDate) != 0;
    }

    /// <summary>
    /// Получить нужное кол-во показаний прибора в данном диапазоне дат
    /// </summary>
    /// <param name="numOfPoint">Количество показаний прибора, которое надо предоставить. Вернёт или данное
    /// количество, или меньше (в случае, если в данном диапазоне дат реальных показаний не так много)</param>
    /// <returns>Нужное кол-вол показания прибора в данном диапазоне дат</returns>
    public List<Data> GetDataBetweenTwoDates(int numOfPoint)
    {
        return _storage.GetDataBatchBetweenTwoDates(beginDate, endDate, numOfPoint);
    }
}