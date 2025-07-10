using MiniERP.Models;

namespace MiniERP.Api.Models
{
    public class Sale
    {

        public int SaleId { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int Quantity { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;
    }
}
