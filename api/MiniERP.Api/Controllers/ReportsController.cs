using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Data;
using MiniERP.Models;

namespace MiniERP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/reports/total-sales
        [HttpGet("total-sales")]
        public async Task<ActionResult<decimal>> GetTotalSales()
        {
            var total = await _context.Sales
                .Include(s => s.Product)
                .SumAsync(s => s.Product.Price * s.Quantity);

            return Ok(total);
        }

        // GET: api/reports/top-products
        [HttpGet("top-products")]
        public async Task<ActionResult> GetTopProducts()
        {
            var topProducts = await _context.Sales
                .GroupBy(s => s.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(s => s.Quantity),
                    ProductName = g.First().Product.Name
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(5)
                .ToListAsync();

            return Ok(topProducts);
        }

        // GET: api/reports/by-date?start=2024-01-01&end=2024-12-31
        [HttpGet("by-date")]
        public async Task<ActionResult> GetSalesByDate([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var sales = await _context.Sales
                .Where(s => s.SaleDate >= start && s.SaleDate <= end)
                .Include(s => s.Product)
                .Include(s => s.Customer)
                .ToListAsync();

            return Ok(sales);
        }
    }
}
