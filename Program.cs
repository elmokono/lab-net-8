using Amazon.DynamoDBv2;
using Amazon.SQS;
using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyAwsApp.Controllers;
using MyAwsApp.Validators;

#region builder
var builder = WebApplication.CreateBuilder(args);

//aws
//dynamo
builder.Services.AddSingleton(sp =>
{
    var config = new AmazonDynamoDBConfig
    {
        ServiceURL = builder.Configuration["DynamoDbSettings:ServiceURL"]
    };
    return new AmazonDynamoDBClient(config);
});

//sqs
builder.Services.AddSingleton<IAmazonSQS>(sp =>
{
    var config = new AmazonSQSConfig
    {
        ServiceURL = builder.Configuration["SQSSettings:ServiceURL"]
    };
    return new AmazonSQSClient(config);
});

//mongodb
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

//users
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddSingleton<IUsersRepository, UsersRepositoryDynDB>(); //dynamodb implementation
builder.Services.AddScoped<UsersController>();

//products
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IProductsQueueService, ProductsQueueService>(); ; //sqs implementation
builder.Services.AddSingleton<IProductsRepository, ProductsRepositoryDynDB>(); //dynamodb implementation
builder.Services.AddScoped<ProductsController>();

//orders
builder.Services.AddSingleton<IOrdersService, OrdersService>();
builder.Services.AddSingleton<IOrdersRepository, OrdersRepositoryMongoDB>(); //mongo implementation
builder.Services.AddScoped<OrdersController>();

//validators
builder.Services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

#endregion

var app = builder.Build();

app.UseExceptionHandler("/error");
app.MapGet("/error", () => Results.Problem("An unexpected error occurred"));

#region controllers
//controllers
new UsersController().MapUsersEndpoints(app);
new ProductsController().MapUsersEndpoints(app);
new OrdersController().MapUsersEndpoints(app);
#endregion

app.Run();