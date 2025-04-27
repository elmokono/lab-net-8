using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyAwsApp.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ClientUserId { get; set; }
        public List<OrderItem> Items { get; set; } = [];
    }
}
