var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
object value = builder.Services.();

var app = builder.Build();
//Tratamento de erro.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => "Hello World!");

app.Run();
