using Microsoft.Extensions.Logging;

namespace ClassLibrary1
{
    public class NoSqlService : INoSqlService
    {
        private string? _connectionType;

        private DataConnections _dc;
        private readonly ILogger<NoSqlService> _logger;
        private List<DataConnections> _dataConnections;
        public DataConnections Connection
        {
            get => _dc!;
            set { _dc = value;  }
        }

        public string ConnectionType
        {
            get => _connectionType!;
            set { _connectionType = value; }
        }
        public NoSqlService(ILogger<NoSqlService> logger)
        {
            _logger = logger;
            _dataConnections = new List<DataConnections>();
            _dataConnections.Add(new DataConnections()
            {
                DataConnectionKey = "connection1",
                DataConnectionValue = new DataConnection()
                {
                    DBName = "db1",
                    DBConnection = "connection db1"
                }
            });


        }

        public void SetConnection(string conType)
        {
            ConnectionType = conType;
            Connection = _dataConnections.Where(x => x.DataConnectionKey == conType).FirstOrDefault()!;


        }
        public string GetvalueConnectionString() {
            _logger.LogInformation($"get value {ConnectionType}");
            return ConnectionType;

        }
        public DataConnections GetConnectionobj()
        {
            _logger.LogInformation($"get value {Connection}");
            return Connection;

        }
    }
}