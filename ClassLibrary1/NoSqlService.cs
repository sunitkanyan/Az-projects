using Microsoft.Extensions.Logging;

namespace ClassLibrary1
{
    public class NoSqlService : INoSqlService
    {
        private string? _connectionType;
        private readonly ILogger<NoSqlService> _logger;
        public string ConnectionType
        {
            get => _connectionType!;
            set { _connectionType = value; }
        }
        public NoSqlService(ILogger<NoSqlService> logger)
        {
            _logger = logger;           
        }

        public void SetConnection(string conType)
        {
            ConnectionType = conType;
            
        }
        public string GetvalueConnectionString() {
            _logger.LogInformation($"get value {ConnectionType}");
            return ConnectionType;

        }
    }
}