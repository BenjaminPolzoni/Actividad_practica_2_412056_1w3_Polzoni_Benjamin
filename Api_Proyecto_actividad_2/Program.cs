using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data;
using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data.Interfaces;
using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Services;

var builder = WebApplication.CreateBuilder(args);

// Ingresar la conexion
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
//    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//// Ingresar las dependencias

//// Dependencia de articulos
builder.Services.AddScoped<IArticleRepository>(provider => new ArticleRepository());
builder.Services.AddScoped<ArticleServices, ArticleServices>();

//// Dependencia de Facturas/Invoices
builder.Services.AddScoped<IInvoiceReposiotry>(provider => new InvoiceRepository());
builder.Services.AddScoped<InvoiceServices>(provider => new InvoiceServices());

//// Dependencia de Vendedores
builder.Services.AddScoped<ISellerRepository>(provider => new SellerRepository());
builder.Services.AddScoped<SellerServices, SellerServices>();

//// Dependencia de Clientes
builder.Services.AddScoped<IClientRepository>(provider => new ClientRepostory());
builder.Services.AddScoped<ClientServices, ClientServices>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
