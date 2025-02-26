using System;

namespace WarehouseApi.Models
{
    public class ProductLog
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }  // อ้างอิงถึงสินค้าที่ถูกเปลี่ยนแปลง
        public string? Action { get; set; }  // "ADD", "DELETE", "UPDATE"
        public string? OldValue { get; set; }  // เก็บค่าก่อนเปลี่ยนแปลง (ถ้ามี)
        public string? NewValue { get; set; }  // เก็บค่าหลังเปลี่ยนแปลง (ถ้ามี)
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // เวลาที่เปลี่ยนแปลง
    }
}
