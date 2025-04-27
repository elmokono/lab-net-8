using Amazon.DynamoDBv2;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyAwsApp.Controllers;

#region builder
var builder = WebApplication.CreateBuilder(args);

//aws
builder.Services.Configure<DynamoDbSettings>(builder.Configuration.GetSection("DynamoDbSettings"));
builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<DynamoDbSettings>>().Value;
    var config = new AmazonDynamoDBConfig
    {
        ServiceURL = settings.ServiceURL
    };
    return new AmazonDynamoDBClient(config);
});

//mongodb
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
#endregion

#region services
//users
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddSingleton<IUsersRepository, UsersRepositoryDynDB>(); //dynamodb implementation
builder.Services.AddScoped<UsersController>();

//products
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddSingleton<IProductsRepository, ProductsRepositoryDynDB>(); //dynamodb implementation
builder.Services.AddScoped<ProductsController>();

//orders
builder.Services.AddSingleton<IOrdersService, OrdersService>();
builder.Services.AddSingleton<IOrdersRepository, OrdersRepositoryMongoDB>(); //mongo implementation
builder.Services.AddScoped<OrdersController>();
#endregion

var app = builder.Build();

#region controllers
//controllers
new UsersController().MapUsersEndpoints(app);
new ProductsController().MapUsersEndpoints(app);
new OrdersController().MapUsersEndpoints(app);
#endregion

app.Run();