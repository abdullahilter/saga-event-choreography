namespace api.stock;

public class Payment
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string CustomerName { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedOn { get; set; }
}