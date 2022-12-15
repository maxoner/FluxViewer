using FluxViewer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZedGraph;

namespace FluxViewer.DataAccess.LiteDbb
{
    /// <summary>
    /// Сервис для работы с no-sql БД: litedb 
    /// </summary>
    public interface ILiteDbService
    {
        /// <summary>
        /// Метод для подключения к БД если она есть, иначе для создания
        /// </summary>
        /// <param name="dataBasePath">Полный путь до местанахождения БД</param>
        /// <returns>Успешно ли открытие/создание</returns>
        public bool ConnectOrCreateDataBase(string dataBasePath);

        /// <summary>
        /// Метод для отключения от текущей БД
        /// </summary>
        public void DisconnectFromDataBase();

        /// <summary>
        /// Записать данные
        /// </summary>
        /// <param name="data"></param>
        public void WriteData(Data data);

        /// <summary>
        /// Записать коллекцию данных
        /// </summary>
        /// <param name="data"></param>
        public void WriteData(IEnumerable<Data> data);

        /// <summary>
        /// Лог информации
        /// </summary>
        public void LogInformation(string message);

        /// <summary>
        /// Лог предупреждения
        /// </summary>
        public void LogWarning(string message);

        /// <summary>
        /// Лог ошибки
        /// </summary>
        public void LogError(string message, Exception exception = default);

        /// <summary>
        /// Получение логов между датами
        /// </summary>
        /// <param name="beginDate">Дата начала</param>
        /// <param name="endDate">Дата конца</param>
        /// <returns></returns>
        public List<Log> GetLogsBetweenTwoDates(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// Получение всех логов
        /// </summary>
        /// <returns></returns>
        public List<Log> GetAllLogs();

        /// <summary>
        /// Получение данных между двумя датами
        /// </summary>
        /// <param name="beginDate">Дата начала</param>
        /// <param name="endDate">Дата конца</param>
        /// <returns>Данные</returns>
        public List<Data> GetDataBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null);
      
        /// <summary>
        /// получание нужного столбца между двумя датами
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="datatype"></param>
        /// <returns></returns>

        public PointPair[] GetDataBetweenTwoDatesColumn(DateTime beginDate, DateTime endDate, int datatype, int? step = null);

        /// <summary>
        /// Получение всех данных
        /// </summary>
        /// <returns>Данные</returns>
        public List<Data> GetAllData();

        /// <summary>
        /// Получение количества данных между двумя датами
        /// </summary>
        /// <param name="beginDate">Дата начала</param>
        /// <param name="endDate">Дата конца</param>
        /// <returns>Количество записей</returns>
        public int GetDataCountBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null);

        public bool GetHasDataBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null);

    }
}
