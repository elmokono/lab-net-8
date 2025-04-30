using Amazon.DynamoDBv2.DataModel;

namespace MyAwsApp.Dtos
{
    [DynamoDBTable("Products")]
    public class ProductDto
    {
        [DynamoDBHashKey]
        public string ProductId { get; set; }
        [DynamoDBProperty] 
        public string Description { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public int Stock { get; set; }
        [DynamoDBProperty]
        public decimal Price { get; set; }
    }
}
