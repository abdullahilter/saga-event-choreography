namespace api.notify;

public class Order
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string CustomerName { get; set; }

    public DateTime CreatedOn { get; set; }
}