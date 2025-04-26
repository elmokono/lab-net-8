namespace MyAwsApp.Models
{
    public class OrderItem
    {
        public string OrderItemId { get; set; }
        public Product Product { get; set; }
        public decimal Amount { get; set; }
    }
}
