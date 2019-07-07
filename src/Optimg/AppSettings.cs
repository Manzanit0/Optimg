using System;

namespace Optimg
{
    public static class AppSettings
    {
        public static string KrakenApiKey => GetOrThrow("OPTIMG_KRAKEN_API_KEY");
        public static string KrakenApiSecret => GetOrThrow("OPTIMG_KRAKEN_API_SECRET");
        public static string AwsAccountKey => GetOrThrow("OPTIMG_AWS_ACCOUNT_KEY");
        public static string AwsSecret => GetOrThrow("OPTIMG_AWS_SECRET");
        public static string S3Bucket => GetOrThrow("OPTIMG_AWS_S3_BUCKET");
        public static string AWSRegion => GetOrThrow("OPTIMG_AWS_REGION");

        private static string GetOrThrow(string environmentVariable)
        {
            return Environment.GetEnvironmentVariable(environmentVariable) ?? 
                   throw new Exception($"{environmentVariable} is empty – please check the environment variables");
        }
    }
}