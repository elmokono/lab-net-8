using Amazon.DynamoDBv2;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyAwsApp.Controllers;
using MyAwsApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

//aws
builder.Services.AddSingleton<AmazonDynamoDBClient>();
//mongodb
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDbSettings"));

//users
builder.Services.AddSingleton<IUsersService, UsersService>();
builder.Services.AddSingleton<IUsersRepository, UsersRepositoryDynDB>(); //dynamodb implementation
builder.Services.AddScoped<UsersController>();

//products
builder.Services.AddSingleton<IProductsService, ProductsService>();
builder.Services.AddSingleton<IProductsRepository, ProductsRepositoryDynDB>(); //dynamodb implementation
builder.Services.AddScoped<ProductsController>();

//orders
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

var app = builder.Build();

//controllers
new UsersController().MapUsersEndpoints(app);
new ProductsController().MapUsersEndpoints(app);


app.Run();