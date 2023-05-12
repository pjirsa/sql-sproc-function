using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Demo.SQL.Functions;

public class CallSproc
{
    private readonly ILogger _logger;

    public CallSproc(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CallSproc>();
    }

    [Function("CallSproc")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var connStr = Environment.GetEnvironmentVariable("sql_connection");
        using var conn = new SqlConnection(connStr);
        conn.Open();
        var cmdText = $"EXEC dbo.SelectProductsCost @Cost = 3";

        using SqlCommand cmd = new SqlCommand(cmdText, conn);
        var result = await cmd.ExecuteScalarAsync();

        _logger.LogInformation(result?.ToString());

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString(result?.ToString());

        return response;
    }

    public record RequestBody(string Msg);

}
