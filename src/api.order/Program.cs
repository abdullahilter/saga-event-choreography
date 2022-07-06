using shared;

namespace api.order;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IMessageProducer, MessageProducer>();
        builder.Services.AddSingleton<IOrderService, OrderService>();
        builder.Services.AddHostedService<StockReducedMessageConsumer>();
        builder.Services.AddHostedService<PaymentRefundedMessageConsumer>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}