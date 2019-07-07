using System;
using System.Net;

using Kraken;
using Kraken.Http;
using OptimizeWaitRequest = Kraken.Model.S3.OptimizeWaitRequest;

namespace Optimg
{
    public class KrakenOptimizer : IOptimizer
    {
        public Client Client { get; set; }
        
        public KrakenOptimizer()
        {
            
            var connection = Connection.Create(
                AppSettings.KrakenApiKey,
                AppSettings.KrakenApiSecret);
            
            Client = new Client(connection);
        }

        public virtual string Optimize(string imageUrl, string destDirectory)
        {
            var request = new OptimizeWaitRequest(
                new Uri(imageUrl),
                AppSettings.AwsAccountKey,
                AppSettings.AwsSecret,
                AppSettings.S3Bucket,
                AppSettings.AWSRegion) {S3Store = {Path = destDirectory}};


            var response = Client.OptimizeWait(request);

            if (response.Result.StatusCode != HttpStatusCode.OK) {
                throw new Exception($"Kraken API Error: {response.Result.Error}");
            }

            return response.Result.Body.KrakedUrl;
        }
    }
}