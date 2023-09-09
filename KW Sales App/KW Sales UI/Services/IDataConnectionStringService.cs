using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW_Sales_UI.Services
{
    public interface IDataConnectionStringService
    {
        string SqlConnectionString { get; }

    }
}
