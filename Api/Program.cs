using Api.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infra.DataContext;
using Store.Infra.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductContext>(options =>
{
    options.UseInMemoryDatabase("Store");
});

builder.Services.AddScoped<ProductContext, ProductContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<ProductViewModel, Product>();
    cfg.CreateMap<Product, ProductViewModel>();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Configuração global de codificação para o aplicativo
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
    });


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
