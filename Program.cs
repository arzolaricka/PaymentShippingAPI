using PaymentShippingDataService;
using PaymentShippingService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPaymentShippingDataService, DbDataService>();
builder.Services.AddScoped<PaymentShippingService.PaymentShippingService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Payment & Shipping API",
        Version = "v1",
        Description = "REST API for managing Payments and Shipping"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment & Shipping API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();