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
        public Client Client { get; set; }
        
        public Optimizer()
        {
            Settings = AppSettings.Load();
            
            var connection = Connection.Create(
                Settings.KrakenApiKey,
                Settings.KrakenApiSecret);
            
            Client = new Client(connection);
        }

        public virtual string Optimize(string imageUrl, string destDirectory)
        {
            var request = new OptimizeWaitRequest(
                new Uri(imageUrl),
                Settings.AwsAccountKey,
                Settings.AwsSecret,
                Settings.S3Bucket,
                Settings.AWSRegion) {S3Store = {Path = destDirectory}};


            var response = Client.OptimizeWait(request);

            if (response.Result.StatusCode != HttpStatusCode.OK) {
                throw new Exception($"Kraken API Error: {response.Result.Error}");
            }

            return response.Result.Body.KrakedUrl;
        }
    }
}