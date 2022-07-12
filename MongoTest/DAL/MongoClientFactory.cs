using System.Collections.Concurrent;
using MongoDB.Bson;
using MongoDB.Driver.Core.Events;

namespace MongoTest.DAL;

 public class MongoClientFactory
    {
        private readonly ILogger<MongoClientFactory> _logger;
        private readonly string _connectionString;

        private static readonly ConcurrentDictionary<long, string> _operationsCommand = new();

        static MongoClientFactory()
        {
            BsonClassMap.RegisterClassMap<VlastnostRidic>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("VlastnostRidic");
            });
            
            BsonClassMap.RegisterClassMap<VlastnostKurak>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("VlastnostKurak");
            });
        }

        public MongoClientFactory(ILogger<MongoClientFactory> logger, string connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        private static readonly Action<ILogger, string, int, string, Exception> _logCommandSucceededEvent = LoggerMessage.Define<string, int, string>(
            LogLevel.Information,
            new EventId(1, nameof(CommandSucceededEvent)),
            "Execute MongoDbCommand: {CommandName}, Duration: {Duration} ms, Command: {Command}"
        );

        private static readonly Action<ILogger, string, int, string, Exception> _logCommandFailedEvent = LoggerMessage.Define<string, int, string>(
            LogLevel.Information,
            new EventId(1, nameof(CommandFailedEvent)),
            "Execute MongoDbCommand: {CommandName}, Duration: {Duration} ms, Command: {Command}"
        );

        public IMongoClient Create()
        {
            var mongoSettings = MongoClientSettings.FromConnectionString(_connectionString);
            mongoSettings.ClusterConfigurator = cc =>
            {
                cc.Subscribe<CommandStartedEvent>(evn =>
                {
                    if (evn.OperationId.HasValue)
                    {
                        _operationsCommand.TryAdd(evn.OperationId.Value, evn.Command.ToJson());
                    }
                });

                cc.Subscribe<CommandSucceededEvent>(evn =>
                {
                    if (evn.OperationId.HasValue && _operationsCommand.TryRemove(evn.OperationId.Value, out var commandJson))
                    {
                        _logCommandSucceededEvent(_logger, evn.CommandName, evn.Duration.Milliseconds, commandJson, null!);
                    }
                });

                cc.Subscribe<CommandFailedEvent>(evn =>
                {
                    if (evn.OperationId.HasValue && _operationsCommand.TryRemove(evn.OperationId.Value, out var commandJson))
                    {
                        _logCommandFailedEvent(_logger, evn.CommandName, evn.Duration.Milliseconds, commandJson, null!);
                    }
                });
            };

            return new MongoClient(mongoSettings);
        }
    }