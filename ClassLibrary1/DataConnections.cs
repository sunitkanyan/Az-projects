using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DataConnections
    {
        public string DataConnectionKey { get; set; }  
        
        public DataConnection DataConnectionValue { get; set; }

        public DataConnections() { }

    }
    public class DataConnection
    {
        public string DBName { get; set; }
        public string DBConnection { get; set; }

        public DataConnection() { }

    }



}
