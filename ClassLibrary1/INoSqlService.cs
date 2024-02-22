using Microsoft.Extensions.Logging;

namespace ClassLibrary1
{
    public interface INoSqlService
    {
        public string GetvalueConnectionString();
        public void SetConnection(string conType);

        public DataConnections GetConnectionobj();
    }
}