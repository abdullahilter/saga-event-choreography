namespace api.order;

public class OrderRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string CustomerName { get; set; }
}