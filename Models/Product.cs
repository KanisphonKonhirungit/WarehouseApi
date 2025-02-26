public class Product
{
    public int Id { get; set; }

    // เพิ่ม required modifier
    public string? Name { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime DateAdded { get; set; }
}
