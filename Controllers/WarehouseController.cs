// Controllers/WarehouseController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApi.Data;
using WarehouseApi.Models;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseContext _context;

        public WarehouseController(WarehouseContext context)
        {
            _context = context;
        }

        // 1. รับสินค้าเข้าคลัง (เพิ่มสินค้า)
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is invalid");
            }

            product.DateAdded = DateTime.Now;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // 2. แสดงสินค้าคงคลังทั้งหมด
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // 3. เบิกสินค้าออกจากคลัง (ลดจำนวนสินค้า)
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawProduct([FromBody] WithdrawRequest request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            if (product.Quantity < request.Quantity)
            {
                return BadRequest("Not enough stock");
            }

            product.Quantity -= request.Quantity;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product withdrawn successfully", remainingStock = product.Quantity });
        }

        // 4. ตรวจสอบรายการ รับสินค้า เบิกสินค้า และสินค้าคงคลัง
        [HttpGet("check")]
        public async Task<ActionResult<IEnumerable<Product>>> CheckProductRecords()
        {
            return await _context.Products.OrderBy(p => p.DateAdded).ToListAsync();
        }

        // ฟังก์ชันช่วยเหลือ
        [HttpGet("product/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }

    // Request Body สำหรับการเบิกสินค้า
    public class WithdrawRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
