using System;
using System.Collections.Generic;
using FluxViewer.DataAccess.Models;
using ZedGraph;

namespace FluxViewer.DataAccess.LiteDbb
{
    /// <summary>
    /// Сервис для работы с no-sql БД: litedb 
    /// </summary>
    public interface ILiteDbService
    {
        /// <summary>
        /// Метод для подключения к базе данных. Если база уже существует, то происходит подключение, иначе - создание. 
        /// </summary>
        /// <param name="dataBasePath">Полный путь до местанахождения БД</param>
        /// <returns>true, если открытие/создание произошло успешно, иначе false</returns>
        public bool ConnectOrCreateDataBase(string dataBasePath);

        /// <summary>
        /// Метод для отключения от текущей базы данных.
        /// </summary>
        public void DisconnectFromDataBase();

        /// <summary>
        /// Получение всех логов из базы данных.
        /// </summary>
        /// <returns>Список логов</returns>
        public List<Log> GetAllLogs();

        /// <summary>
        /// Получение логов между датами из базы данных.
        /// </summary>
        /// <param name="beginDate">Дата начала, с которой следует искать логи</param>
        /// <param name="endDate">Дата конца, по которую следует искать логи</param>
        /// <returns></returns>
        public List<Log> GetLogsBetweenTwoDates(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// Запись информационного лога в базу данных.
        /// </summary>
        public void LogInformation(string message);

        /// <summary>
        /// Запись предупреждающего лога в базу данных.
        /// </summary>
        public void LogWarning(string message);

        /// <summary>
        /// Запись лога с ошибкой в базу данных.
        /// </summary>
        public void LogError(string message, Exception exception = default);

        /// <summary>
        /// Получение всех показаний прибора из базы данных.
        /// </summary>
        /// <returns>Все показания прибора</returns>
        public List<Data> GetAllData();


        /// <summary>
        /// Получение показаний прибора из базы данных между двумя датами.
        /// </summary>
        /// <param name="beginDate">Дата начала, с которой следует искать показания</param>
        /// <param name="endDate">Дата конца, по которую следует искать показания</param>
        /// <returns>Все показания прибора в текущий временной интервал</returns>
        public List<Data> GetDataBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null);

        /// <summary>
        /// Получение количества показаний между двумя датами.
        /// </summary>
        /// <param name="beginDate">Дата начала, с которой считаются показания</param>
        /// <param name="endDate">Дата конца, по которую считаются показания</param>
        /// <returns>Количество показаний прибора</returns>
        public int GetDataCountBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null);

        /// <summary>
        /// Проверка на то, есть ли показания прибора между указанными датами?
        /// </summary>
        /// <param name="beginDate">Дата начала, с которой считаются показания</param>
        /// <param name="endDate">Дата конца, по которую считаются показания</param>
        /// <param name="step"></param>
        /// <returns>true - если записи между датами есть, false - если записей не обнаружено</returns>
        public bool GetHasDataBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null);

        /// <summary>
        /// Получание нужного столбца из базы данных между двумя датами.
        /// </summary>
        /// <param name="beginDate">Дата начала, с которой следует искать показания</param>
        /// <param name="endDate">Дата конца, по которую следует искать показания</param>
        /// <param name="datatype">Тип данных, которые требуется вернуть</param>
        /// <returns>?</returns>
        /// TODO: переделать данный метод (как минимум, не передавать dataType в виде int-а)
        public PointPair[] GetDataBetweenTwoDatesColumn(DateTime beginDate, DateTime endDate, int datatype,
            int? step = null);

        /// <summary>
        /// Добавление показаний прибора в базу данных.
        /// </summary>
        /// <param name="data">Структура, описывающая 1 показание прибора</param>
        public void WriteData(Data data);

        /// <summary>
        /// Записать коллекцию показаний прибора в базу данных.
        /// </summary>
        /// <param name="data">Коллекция показаний прибора</param>
        public void WriteData(IEnumerable<Data> data);
    }
}