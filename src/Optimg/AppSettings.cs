using System;

namespace Optimg
{
    public class AppSettings
    {
        public string KrakenApiKey { get; set; }
        public string KrakenApiSecret { get; set; }
        public string AwsAccountKey { get; set; }
        public string AwsSecret { get; set; }
        public string S3Bucket { get; set; }
        public string AWSRegion { get; set; }
        
        public static AppSettings Load()
        {
            var settings = new AppSettings();
            
            settings.AwsSecret = Environment.GetEnvironmentVariable("OPTIMG_AWS_SECRET");
            settings.AwsAccountKey= Environment.GetEnvironmentVariable("OPTIMG_AWS_ACCOUNT_KEY");
            settings.S3Bucket = Environment.GetEnvironmentVariable("OPTIMG_AWS_S3_BUCKET");
            settings.AWSRegion = Environment.GetEnvironmentVariable("OPTIMG_AWS_REGION");
            settings.KrakenApiKey = Environment.GetEnvironmentVariable("OPTIMG_KRAKEN_API_KEY");
            settings.KrakenApiSecret = Environment.GetEnvironmentVariable("OPTIMG_KRAKEN_API_SECRET");

            ThrowIfEmptySettings(settings);

            return settings;
        }

        public static void ThrowIfEmptySettings(AppSettings settings)
        {
            var emptyMessage = "is empty – please check the environment variables";
            
            if (String.IsNullOrEmpty(settings.AwsSecret))
            {
                throw new Exception($"AWS Secret {emptyMessage}");
            }
            
            if (String.IsNullOrEmpty(settings.AwsAccountKey))
            {
                throw new Exception($"AWS Account Key {emptyMessage}");
            }
            
            if (String.IsNullOrEmpty(settings.KrakenApiKey))
            {
                throw new Exception($"Kraken API Key {emptyMessage}");
            }
            
            if (String.IsNullOrEmpty(settings.KrakenApiSecret))
            {
                throw new Exception($"Kraken API Secret {emptyMessage}");
            }
            
            if (String.IsNullOrEmpty(settings.S3Bucket))
            {
                throw new Exception($"S3 Bucket name {emptyMessage}");
            }
            
            if (String.IsNullOrEmpty(settings.AWSRegion))
            {
                throw new Exception($"S3 Bucket region {emptyMessage}");
            }
        }
    }
}