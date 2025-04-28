using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyAwsApp.Dtos
{
    public class OrderDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ClientUserId { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];
    }
}
