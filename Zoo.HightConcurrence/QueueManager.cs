using StackExchange.Redis;
using System;
using System.Configuration;

namespace HighConcurrence.App
{
    public class QueueManager
    {
        private static Lazy<ConnectionMultiplexer> _connectionMultiplexer;
        private readonly string _queueKey = "QueueManager";
        public QueueManager()
        {
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() =>
            {
                var connectionString = ConfigurationManager.AppSettings["Redis.ConnectionString"];
                if (string.IsNullOrEmpty(connectionString))
                    throw new ConfigurationErrorsException("配置文件中无法找到设置项[Redis.ConnectionString]");
                return ConnectionMultiplexer.Connect(connectionString);
            });
        }
        private static QueueManager _instance;
        private static object _sync = new object();
        public static QueueManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new QueueManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private static IDatabase Database
        {
            get { return _connectionMultiplexer.Value.GetDatabase(); }
        }

        public void AddQueue(string queueName)
        {
            if (queueName == null)
                throw new ArgumentNullException(nameof(queueName));
            Database.SetAdd(_queueKey, queueName);
        }
        public long QueueLength()
        {
            return Database.SetLength(_queueKey);
        }
        public void RemoveQueue(string value)
        {
            Database.SetRemove(_queueKey, value);
        }
    }
}
