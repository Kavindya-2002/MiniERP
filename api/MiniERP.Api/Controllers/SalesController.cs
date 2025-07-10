using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Models;
using MiniERP.Data;
using MiniERP.Models;

namespace MiniERP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .ToListAsync();
        }

        // POST: api/sales
        [HttpPost]
        public async Task<ActionResult<Sale>> CreateSale(Sale sale)
        {
            // Get the product
            var product = await _context.Products.FindAsync(sale.ProductId);
            if (product == null) return NotFound("Product not found.");

            if (product.Quantity < sale.Quantity)
                return BadRequest("Not enough stock.");

            // Reduce stock
            product.Quantity -= sale.Quantity;

            // Add sale to DB
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSales), new { id = sale.SaleId }, sale);
        }
    }
}
