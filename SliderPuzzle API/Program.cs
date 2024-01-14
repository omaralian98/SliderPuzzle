using Microsoft.AspNetCore.Http.Timeouts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRequestTimeouts();
builder.Services.AddControllers();

var app = builder.Build();
app.MapGet("/attribute",
    [RequestTimeout(milliseconds: 2000)] async (HttpContext context) => {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(10), context.RequestAborted);
        }
        catch (TaskCanceledException)
        {
            return Results.Content("Timeout!", "text/plain");
        }

        return Results.Content("No timeout!", "text/plain");
    });
app.UseRequestTimeouts();

app.MapControllers();
app.Run();
