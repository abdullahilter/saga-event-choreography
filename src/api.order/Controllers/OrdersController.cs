using Microsoft.AspNetCore.Mvc;
using shared;

namespace api.order.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMessageProducer _messageProducer;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(
        IOrderService orderService,
        IMessageProducer messageProducer,
        ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _messageProducer = messageProducer;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] OrderRequest model)
    {
        var order = await _orderService.CreateAsync(model);

        _messageProducer.Publish(order, "order.created");

        _logger.LogInformation("order request accepted at {date}", DateTime.Now.ToString("O"));

        return Accepted();
    }
}