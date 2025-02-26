using System;

namespace WarehouseApi.Models
{
    public class ProductLog
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? Action { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
