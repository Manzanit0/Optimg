using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using JsonSerializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace Optimg 
{
    public class Function
    {

        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            LogMessage(context, "Processing request started");
            return CreateResponse(DateTime.Now);
        }

        private APIGatewayProxyResponse CreateResponse(DateTime? result)
        {
            var statusCode = result != null ? (int) HttpStatusCode.OK : (int) HttpStatusCode.InternalServerError;

            var body = result != null ? JsonConvert.SerializeObject(result) : string.Empty;

            var response = new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = body,
                Headers = new Dictionary<string, string>
                {
                    {"Content-Type", "application/json"},
                    {"Access-Control-Allow-Origin", "*"}
                }
            };

            return response;
        }
        
        private void LogMessage(ILambdaContext ctx, string msg)
        {
            ctx.Logger.LogLine($"{ctx.AwsRequestId}:{ctx.FunctionName} - {msg}");
        }
    }
}