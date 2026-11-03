
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

bool isOrderCompleted = false;

// Long polling endpoint
app.MapGet("/order/status", async (CancellationToken token) =>
{
    Console.WriteLine("Client connected. Waiting for order...");

    // Wait until order completes or 20 seconds pass
    for (int i = 0; i < 20; i++)
    {
        if (isOrderCompleted)
        {
            return Results.Ok("Order Completed!");
        }

        await Task.Delay(1000, token);
    }

    return Results.NoContent();
});

// Update order status
app.MapPost("/order/complete", () =>
{
    isOrderCompleted = true;

    return Results.Ok("Order marked as completed");
});

app.Run();
