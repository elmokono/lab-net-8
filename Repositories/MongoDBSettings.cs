namespace MyAwsApp.Repositories
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string OrdersCollection { get; set; }
    }
}
