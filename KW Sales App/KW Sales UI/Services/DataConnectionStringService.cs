using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW_Sales_UI.Services
{
    public class DataConnectionStringService : IDataConnectionStringService
    {
        private string? _sqlConnectionString;
        public string SqlConnectionString
        {
            get
            {
                return _sqlConnectionString!;
            }
            private set
            {
                _sqlConnectionString = value;
            }
        }

        public DataConnectionStringService()
        {
            SqlConnectionString = ConfigurationManager.ConnectionStrings["SQLKWSALESDB"].ConnectionString;
        }
    }
}
