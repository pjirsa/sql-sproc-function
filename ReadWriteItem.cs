using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace Demo.SQL.Functions
{
    public class ReadWriteItem
    {
        private readonly ILogger _logger;

        public ReadWriteItem(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ReadWriteItem>();
        }        

        [Function("GetAndAddProducts")]
        [SqlOutput("ProductsWithIdentity", "sql_connection")]
        public IEnumerable<Product> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getandaddproducts/{cost}")]
            HttpRequestData req,
            [SqlInput("SELECT * FROM Products",
                "sql_connection",
                parameters: "@Cost={cost}")]
            IEnumerable<Product> products)
        {
            _logger.LogInformation($"{products.Count()} found. Writing them to 'ProductsWithIdentity' table.");
            return products.ToArray();
        }
    }
}
