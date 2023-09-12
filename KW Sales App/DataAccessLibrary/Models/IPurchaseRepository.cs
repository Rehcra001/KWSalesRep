using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public interface IPurchaseRepository
    {
        string ConnectionString { get; }
        string Add(PurchaseModel purchase);
    }
}
