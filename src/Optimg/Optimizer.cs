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

        public virtual string Optimize(string imageUrl, string destDirectory)
        {
            var request = new OptimizeWaitRequest(
                new Uri(imageUrl),
                Settings.AwsAccountKey,
                Settings.AwsSecret,
                Settings.S3Bucket,
                Settings.S3BucketRegion) {S3Store = {Path = destDirectory}};


            var response = client.OptimizeWait(request);

            if (response.Result.StatusCode != HttpStatusCode.OK) {
                // TODO Log something
                // response.ResultOnSuccess.Error/StatusCode
            }

            return response.Result.Body.KrakedUrl;
        }
    }
}