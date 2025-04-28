namespace MyAwsApp.Dtos
{
    public class OrderItemDto
    {
        public string OrderItemId { get; set; }
        public ProductDto Product { get; set; }
        public decimal Amount { get; set; }
    }
}
