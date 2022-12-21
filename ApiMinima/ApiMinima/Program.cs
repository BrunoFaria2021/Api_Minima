using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Serviço de injeção de dependecia

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("Cliente"));

var app = builder.Build();

//Tratamento de erro do desenvolvedor.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

//endpoint

app.MapGet("/Clientes", async(AppDbContext dbContext )=>
        await dbContext.Clientes.ToListAsync());

app.MapGet("/Clientes/{id}", async (int id, AppDbContext dbContext) =>
        await dbContext.Clientes.FirstOrDefaultAsync(a => a.Id == id));

app.MapPost("/Clientes", async (Cliente cliente, AppDbContext dbContext) =>
{
    dbContext.Clientes.Add(cliente);
    await dbContext.SaveChangesAsync();

    return cliente;
});

app.MapPut("/Clientes/{id}", async(int id, Cliente cliente,
     AppDbContext dbContext) =>
{
    dbContext.Entry(cliente).State = EntityState.Modified;
    await dbContext.SaveChangesAsync();

    return cliente;
});

app.MapDelete("/Clientes/{id}", async (int id,AppDbContext dbContext) =>
{
    var cliente = await dbContext.Clientes.FirstOrDefaultAsync(a => a.Id == id);

    if (cliente != null)
    {
        dbContext.Clientes.Remove(cliente);
        await dbContext.SaveChangesAsync();
    }
    return;
});


app.UseSwaggerUI();

app.Run();

//Record são tipo de referencia que usa a igualdade baseada em valores

public class Cliente
{
    public int Id { get; set; }
    public string? Nome { get; set; }
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Cliente> Clientes { get; set; }
}
