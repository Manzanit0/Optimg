using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Xunit;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using static Amazon.S3.Util.S3EventNotification;

namespace Optimg.Tests
{
    public class FunctionTest : IDisposable 
    {
        public IAmazonS3 S3Client { get; set; }
        public string BucketName { get; set; }
        public string Key { get; set; }
        
        public FunctionTest()
        {
            InitAWSResources();
        }

        private async void InitAWSResources()
        {
            S3Client = new AmazonS3Client(RegionEndpoint.USWest1);

            BucketName = "lambda-".ToLower() + DateTime.Now.Ticks;
            Key = "images/jake-the-dog.png";

            // Create a bucket an object to setup a test data.
            await S3Client.PutBucketAsync(BucketName);
            
            await S3Client.PutObjectAsync(new PutObjectRequest
            {
                BucketName = BucketName,
                Key = Key,
                ContentBody = "sample data"
            });
        }

        public async void Dispose()
        {
            await AmazonS3Util.DeleteS3BucketWithObjectsAsync(S3Client, BucketName);
        }
        
        [Fact]
        public async Task TestS3EventLambdaFunction()
        {
            // Setup the S3 event object that S3 notifications would create with the
            // fields used by the Lambda function.
            var s3Event = new S3Event
            {
                Records = new List<S3EventNotificationRecord>
                {
                    new S3EventNotificationRecord
                    {
                        AwsRegion = "eu-west-1",
                        S3 = new S3Entity
                        {
                            Bucket = new S3BucketEntity {Name = BucketName },
                            Object = new S3ObjectEntity {Key = Key }
                        }
                    }
                }
            };

            // Invoke the lambda function and confirm the content type was returned.
            var function = new Function(new OptimizerStub());
            var result = await function.FunctionHandler(s3Event, null);

            var imageUrl = $"https://s3.eu-west-1.amazonaws.com/{BucketName}/{Key}";
            var optimizedSlug = "optimized-images/jake-the-dog.png";
            var expected = $"{imageUrl} <> {optimizedSlug}";
            
            Assert.Equal(expected, result);
        }
    }
}
