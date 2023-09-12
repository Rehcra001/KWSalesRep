using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public interface IProductRepository
    {
        string ConnectionString { get; set; }
        (ObservableCollection<ProductModel>, string) GetAll();
        string UpdateTotalQuantitySold(int productID, int quantity);
    }
}
