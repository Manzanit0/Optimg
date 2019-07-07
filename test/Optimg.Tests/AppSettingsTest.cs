using System;
using Xunit;

namespace Optimg.Tests
{
    public class AppAppSettingsTest
    {

        [Fact]
        public void TestSettingLoad()
        {
            Environment.SetEnvironmentVariable("OPTIMG_AWS_SECRET", "AWS_SECRET");
            Environment.SetEnvironmentVariable("OPTIMG_AWS_ACCOUNT_KEY", "AWS_ACCOUNT_KEY");
            Environment.SetEnvironmentVariable("OPTIMG_AWS_S3_BUCKET", "S3_BUCKET");
            Environment.SetEnvironmentVariable("OPTIMG_AWS_REGION", "AWS_REGION");
            Environment.SetEnvironmentVariable("OPTIMG_KRAKEN_API_KEY", "API_KEY");
            Environment.SetEnvironmentVariable("OPTIMG_KRAKEN_API_SECRET", "API_SECRET");

            Assert.Equal(AppSettings.AwsSecret, "AWS_SECRET");
            Assert.Equal(AppSettings.AwsAccountKey, "AWS_ACCOUNT_KEY");
            Assert.Equal(AppSettings.AWSRegion, "AWS_REGION");
            Assert.Equal(AppSettings.S3Bucket, "S3_BUCKET");
            Assert.Equal(AppSettings.KrakenApiKey, "API_KEY");
            Assert.Equal(AppSettings.KrakenApiSecret, "API_SECRET");
        }
        
        [Fact]
        public void TestEmptyValues()
        {
            Environment.SetEnvironmentVariable("OPTIMG_AWS_SECRET", "");
            Environment.SetEnvironmentVariable("OPTIMG_AWS_ACCOUNT_KEY", "");
            Environment.SetEnvironmentVariable("OPTIMG_AWS_S3_BUCKET", "");
            Environment.SetEnvironmentVariable("OPTIMG_AWS_REGION", "");
            Environment.SetEnvironmentVariable("OPTIMG_KRAKEN_API_KEY", "");
            Environment.SetEnvironmentVariable("OPTIMG_KRAKEN_API_SECRET", "");

            Assert.Throws<Exception>(() => AppSettings.AwsSecret);
            Assert.Throws<Exception>(() => AppSettings.AwsAccountKey);
            Assert.Throws<Exception>(() => AppSettings.AWSRegion);
            Assert.Throws<Exception>(() => AppSettings.S3Bucket);
            Assert.Throws<Exception>(() => AppSettings.KrakenApiKey);
            Assert.Throws<Exception>(() => AppSettings.KrakenApiSecret);
        }
        
        [Fact]
        public void TestNonExistentVariables()
        {
            Assert.Throws<Exception>(() => AppSettings.AwsSecret);
            Assert.Throws<Exception>(() => AppSettings.AwsAccountKey);
            Assert.Throws<Exception>(() => AppSettings.AWSRegion);
            Assert.Throws<Exception>(() => AppSettings.S3Bucket);
            Assert.Throws<Exception>(() => AppSettings.KrakenApiKey);
            Assert.Throws<Exception>(() => AppSettings.KrakenApiSecret);
        }
    }
}