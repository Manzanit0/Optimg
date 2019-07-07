using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using JsonSerializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace Optimg 
{
    public class Function
    {
        public IOptimizer Optimizer { get; set; }
        
        public Function()
        {
            Optimizer = new KrakenOptimizer();
        }

        public Function(IOptimizer optimizer)
        {
            Optimizer = optimizer;
        }

        public async Task<string> FunctionHandler(S3Event evt, ILambdaContext context)
        {
            var s3Event = evt.Records?[0].S3;
            var region = evt.Records?[0].AwsRegion;
            if(s3Event == null) return null;

            var imageUrl = $"https://s3.{region}.amazonaws.com/{s3Event.Bucket.Name}/{s3Event.Object.Key}";
            
            try
            {
                return Optimizer.Optimize(imageUrl, CreateOutputUrl(s3Event.Object.Key));
            }
            catch(Exception e)
            {
                context.Logger.LogLine($"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}." +
                                       "Make sure they exist and your bucket is in the same region as this function.");
                context.Logger.LogLine(e.Message);
                context.Logger.LogLine(e.StackTrace);
                throw;
            }
        }

        private String CreateOutputUrl(string key)
        {
            return $"{key}".Replace("images/", "optimized-images/");
        }
    }
}