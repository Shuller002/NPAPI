var builder = WebApplication.CreateBuilder(args);

// ���������� ����� ��� ������������ � Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // ��� ������ Swagger � ������������ API
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ��������� Swagger � ������ ����������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
        options.RoutePrefix = string.Empty; // ������� Swagger �� �������� URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
