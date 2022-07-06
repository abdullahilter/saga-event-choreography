using shared;

namespace api.payment;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IMessageProducer, MessageProducer>();
        builder.Services.AddSingleton<IPaymentService, PaymentService>();
        builder.Services.AddHostedService<OrderCreatedMessageConsumer>();
        builder.Services.AddHostedService<StockOutOfStockMessageConsumer>();

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