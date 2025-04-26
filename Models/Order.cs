namespace MyAwsApp.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ClientUserId { get; set; }
        public List<OrderItem> Items { get; set; } = [];
    }
}
