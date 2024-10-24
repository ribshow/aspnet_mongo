using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace aspnet_mongo.Models
{
    public class ContextMongoDb
    {
        public static string? ConnectionString { get; set; }
        public static bool IsSSL { get; set;}

        public static string? DatabaseName { get; set;}
        private IMongoDatabase _database { get; }

        public ContextMongoDb()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings()
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                    };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);

            }
            catch (Exception) 
            {
                throw new Exception("Não foi possível estabelecer conexão");
            }
        }

        public IMongoCollection<ApplicationUser> User => _database.GetCollection<ApplicationUser>("User");

        public IMongoCollection<Product> Product => _database.GetCollection<Product>("Product");
    }
}
