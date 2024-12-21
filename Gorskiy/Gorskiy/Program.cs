var builder = WebApplication.CreateBuilder(args);

// Добавление служб для контроллеров и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Для работы Swagger с минимальными API
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Включение Swagger в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
        options.RoutePrefix = string.Empty; // Открыть Swagger на корневом URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
