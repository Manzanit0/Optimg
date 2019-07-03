using System;
using System.Net;

using Kraken;
using Kraken.Http;
using OptimizeWaitRequest = Kraken.Model.S3.OptimizeWaitRequest;

namespace Optimg
{
    public class Optimizer
    {
        public AppSettings Settings { get; set; }
        public Client client { get; set; }
        
        public Optimizer()
        {
            Settings = AppSettings.Load();
            
            var connection = Connection.Create(
                Settings.KrakenApiKey,
                Settings.KrakenApiSecret);
            
            client = new Client(connection);
        }
        
        public string Optimize(string imageUrl)
        {
            var request = new OptimizeWaitRequest(
                new Uri(imageUrl),
                Settings.AwsAccountKey,
                Settings.AwsSecret,
                Settings.S3Bucket,
                Settings.S3BucketRegion);
            
            var response = client.OptimizeWait(request);

            if (response.Result.StatusCode != HttpStatusCode.OK) {
                // TODO Log something
            }

            return response.Result.Body.KrakedUrl;
        }
    }
}