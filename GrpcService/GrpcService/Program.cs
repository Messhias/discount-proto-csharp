using GrpcService;
using Microsoft.EntityFrameworkCore;
using DiscountService = GrpcService.Services.DiscountService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<DiscountDbContext>(options =>
	options.UseSqlite("Data Source=discounts.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/",
	() =>
		"gRPC is running :)");

app.Run();