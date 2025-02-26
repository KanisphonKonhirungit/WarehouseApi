using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApi.Models;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var log = new ProductLog
            {
                ProductId = product.Id,
                Action = "ADD",
                NewValue = $"Name: {product.Name}, Quantity: {product.Quantity}, Price: {product.Price}"
            };

            _context.ProductLogs.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return NotFound();

            var oldValue = $"Name: {existingProduct.Name}, Quantity: {existingProduct.Quantity}, Price: {existingProduct.Price}";

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;

            _context.Entry(existingProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var log = new ProductLog
                {
                    ProductId = id,
                    Action = "UPDATE",
                    OldValue = oldValue,
                    NewValue = $"Name: {product.Name}, Quantity: {product.Quantity}, Price: {product.Price}"
                };
                _context.ProductLogs.Add(log);
                await _context.SaveChangesAsync();

                return Ok(existingProduct);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }




       [HttpDelete("{id}")]
public async Task<IActionResult> DeleteProduct(int id)
{
    // ค้นหาผลิตภัณฑ์ที่ต้องการลบ
    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
        return NotFound(); // ถ้าหาสินค้าไม่เจอ
    }

    try
    {
        // ลบข้อมูลในตาราง Products
        _context.Products.Remove(product);

        // บันทึกการลบสินค้า
        await _context.SaveChangesAsync();

        // ส่งข้อมูลการลบกลับ
        return Ok(new { message = "Product deleted successfully", product });
    }
    catch (Exception ex)
    {
        // กรณีเกิดข้อผิดพลาดในการลบข้อมูล
        return StatusCode(500, new { message = "An error occurred while deleting the product.", error = ex.Message });
    }
}




        [HttpGet("logs")]
        public async Task<IActionResult> GetProductLogs()
        {
            var logs = await _context.ProductLogs.ToListAsync(); // ดึงข้อมูลทั้งหมดจาก ProductLogs
            return Ok(logs); // ส่งกลับข้อมูล log ทั้งหมด
        }


    }
}
