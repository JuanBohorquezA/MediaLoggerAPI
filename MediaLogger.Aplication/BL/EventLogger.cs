using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Enumerables;
using MongoDB.Driver;
using System.Collections;


namespace MediaLogger.Aplication.BL
{
    public static class EventLogger
    {
        private static string? _connectionString;
        private static MongoClientSettings? _clientSettings;
        private static IMongoDatabase? _dataBase;
        private static IMongoCollection<Logs>? _collection;
        public static void InitConnection(string connectionString, string DataBase, string collectionName)
        {
            _connectionString = connectionString;
            _clientSettings = MongoClientSettings.FromConnectionString(_connectionString);
            _clientSettings.ConnectTimeout = TimeSpan.FromSeconds(3);

            var client = new MongoClient(_clientSettings);
            _dataBase = client.GetDatabase(DataBase);
            _collection = _dataBase.GetCollection<Logs>(collectionName);
        }
        public static async Task AsyncSaveLog(ETypeLogApp typeLog, string Message, string Ip = "")
        {
            if (_collection == null) return;

            var Log = new Logs
            {
                IP = Ip,
                Message = Message,
                typeLog = typeLog,
                Timestamp = DateTime.UtcNow
            };
            await _collection.InsertOneAsync(Log);
        }
    }
}
