using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public interface IOrderRepository
    {
        string ConnectionString { get; }
        (OrderModel, string) Add(OrderModel order);
    }
}
