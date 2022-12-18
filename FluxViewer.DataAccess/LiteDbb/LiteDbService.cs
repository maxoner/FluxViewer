using System;
using System.Collections.Generic;
using System.Linq;
using FluxViewer.DataAccess.Enums;
using FluxViewer.DataAccess.Models;
using LiteDB;
using ZedGraph;

namespace FluxViewer.DataAccess.LiteDbb
{
    /// <inheritdoc/>
    public class LiteDbService : ILiteDbService
    {
        /// <summary>
        /// Путь до БД
        /// </summary>
        private string _databasePath; // = "defaultDataBase.db";

        private ILiteDatabase _dataContext;

        private ILiteCollection<Log> _logCollection;

        private ILiteCollection<Data> _dataCollection;

        public LiteDbService()
        {
        }

        public bool ConnectOrCreateDataBase(string dataBasePath /* = null*/)
        {
            if (dataBasePath != null)
            {
                _databasePath = dataBasePath;
            }

            if (_dataContext != null)
            {
                _dataContext.Dispose();
                _dataContext = null;
            }

            try
            {
                _dataContext = new LiteDatabase(_databasePath);
                InitializeDataCollection();
                InitializeLogCollection();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void InitializeDataCollection()
        {
            _dataCollection = _dataContext.GetCollection<Data>("Data");
            _dataCollection.EnsureIndex(x => x.DateTime);
            _dataCollection.EnsureIndex(x => x.Id);

            var q = _dataCollection.Query();
        }

        private void InitializeLogCollection()
        {
            _logCollection = _dataContext.GetCollection<Log>("Log");
            _dataCollection.EnsureIndex(x => x.DateTime);
        }

        public void DisconnectFromDataBase()
        {
            InsertLog(LogLevel.Informtaion, "Database disconnected");

            _dataContext.Dispose();
            _dataContext = null;
        }

        public List<Data> GetAllData()
        {
            return _dataCollection.FindAll().ToList();
        }

        public List<Data> GetAllDataBatch(int batchNumber, int batchSize)
        {
            try
            {
                return _dataCollection.Find(Query.All(), skip: batchNumber * batchSize, limit: batchSize).ToList();
            }
            catch (Exception e)
            {
                // TODO Сделай уникальное исключение для этого
                throw new Exception("Оп, а такого батча то и нету");
            }
        }

        public List<Log> GetAllLogs()
        {
            return _logCollection.FindAll().ToList();
        }


        public List<Data> GetDataBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null)
        {
            if (!step.HasValue)
            {
                return _dataCollection.Find(x => x.DateTime > beginDate && x.DateTime < endDate).ToList();
            }
            else
            {
                return _dataCollection.Find(x =>
                    x.DateTime > beginDate && x.DateTime < endDate && (step == null || x.Id % step == 0)).ToList();
            }
        }

        public List<Data> GetDataBatchBetweenTwoDates(DateTime beginDate, DateTime endDate, int batchNumber,
            int batchSize)
        {
            try
            {
                return _dataCollection.Find(x => x.DateTime > beginDate && x.DateTime < endDate,
                    skip: batchNumber * batchSize, limit: batchSize).ToList();
            }
            catch (Exception e)
            {
                // TODO Сделай уникальное исключение для этого
                throw new Exception("Оп, а такого батча то и нету");
            }
        }

        public int GetDataCountBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null)
        {
            if (!step.HasValue)
            {
                return _dataCollection.Count(x => x.DateTime > beginDate && x.DateTime < endDate);
            }
            else
            {
                return _dataCollection.Count(x =>
                    x.DateTime > beginDate && x.DateTime < endDate && (step == null || x.Id % step == 0));
            }
        }

        public bool GetHasDataBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null)
        {
            string q;
            if (step == null)
            {
                q =
                    $"SELECT COUNT($) FROM Data WHERE DateTime > DATETIME('{beginDate}') AND DateTime < DATETIME('{endDate}');";
            }
            else
            {
                q =
                    $"SELECT COUNT($) FROM Data WHERE DateTime > DATETIME('{beginDate}') AND DateTime < DATETIME('{endDate}') AND _id % {step} = 0;";
            }

            using var reader = _dataContext.Execute(q);
            return reader.HasValues;
        }

        public PointPair[] GetDataBetweenTwoDatesColumn(DateTime beginDate, DateTime endDate, int datatype,
            int? step = null)
        {
            string selectPart;

            switch (datatype)
            {
                case 1:
                {
                    selectPart = " DateTime, FLuxSensorData";
                }
                    break;
                case 2:
                {
                    selectPart = " DateTime, TempSensorData";
                }
                    break;
                case 3:
                {
                    selectPart = " DateTime, PressureSensorData";
                }
                    break;
                case 4:
                {
                    selectPart = " DateTime, HumiditySensorData";
                }
                    break;
                default:
                {
                    throw new ArgumentException($"Unknown data type: {datatype}");
                }
            }

            string q;

            if (step == null)
            {
                q =
                    $"SELECT {selectPart} FROM Data WHERE DateTime > DATETIME('{beginDate}') AND DateTime < DATETIME('{endDate}');";
            }
            else
            {
                q =
                    $"SELECT {selectPart} FROM Data WHERE DateTime > DATETIME('{beginDate}') AND DateTime < DATETIME('{endDate}') AND _id % {step} = 0;";
            }

            using var reader = _dataContext.Execute(q);
            var data = reader.Current;
            var arr = data.AsArray;
            var vals = new List<PointPair>();

            while (reader.Read())
            {
                switch (datatype)
                {
                    case 1:
                    {
                        vals.Add(new PointPair(new XDate(reader.Current["DateTime"].AsDateTime),
                            reader.Current["FluxSensorData"].AsDouble));
                    }
                        break;
                    case 2:
                    {
                        vals.Add(new PointPair(new XDate(reader.Current["DateTime"].AsDateTime),
                            reader.Current["TempSensorData"].AsDouble));
                    }
                        break;
                    case 3:
                    {
                        vals.Add(new PointPair(new XDate(reader.Current["DateTime"].AsDateTime),
                            reader.Current["PressureSensorData"].AsDouble));
                    }
                        break;
                    case 4:
                    {
                        vals.Add(new PointPair(new XDate(reader.Current["DateTime"].AsDateTime),
                            reader.Current["HumiditySensorData"].AsDouble));
                    }
                        break;
                    default:
                        throw new ArgumentException($"Unknown data type: {datatype}");
                }
            }

            return vals.ToArray();
        }

        public List<Log> GetLogsBetweenTwoDates(DateTime beginDate, DateTime endDate)
        {
            return _logCollection.Find(x => x.DateTime > beginDate && x.DateTime < endDate).ToList();
        }

        public void LogError(string message, Exception exception = default)
        {
            CheckConnection();

            if (exception != default)
            {
                InsertLogWithException(message, exception);
                return;
            }

            InsertLog(LogLevel.Error, message);
        }

        public void LogInformation(string message)
        {
            CheckConnection();

            InsertLog(LogLevel.Informtaion, message);
        }

        public void LogWarning(string message)
        {
            CheckConnection();

            InsertLog(LogLevel.Warning, message);
        }

        public void WriteData(Data data)
        {
            CheckConnection();

            _dataCollection.Insert(data);
        }

        public void WriteData(IEnumerable<Data> data)
        {
            CheckConnection();

            _dataCollection.Insert(data);
        }

        private void CheckConnection()
        {
            if (_dataContext == null)
            {
                var isSuccess = ConnectOrCreateDataBase(_databasePath);

                if (!isSuccess)
                {
                    throw new Exception("Failed to connect or create database");
                }
            }
        }

        private void InsertLog(LogLevel logLevel, string message)
        {
            _logCollection.Insert(new Log
            {
                Level = logLevel,
                Message = message
            });
        }

        private void InsertLogWithException(string message, Exception exception)
        {
            InsertLog(LogLevel.Error,
                $"{message} |MESSAGE: {exception?.Message} |INNER MESSAEG: {exception?.InnerException?.Message} |STACK TRACE: {exception?.StackTrace}");
        }
    }
}