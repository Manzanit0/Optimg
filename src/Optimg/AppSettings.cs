using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Optimg
{
    public class AppSettings
    {
        public string KrakenApiKey { get; set; }
        public string KrakenApiSecret { get; set; }
        public string AwsAccountKey { get; set; }
        public string AwsSecret { get; set; }
        public string S3Bucket { get; set; }
        public string S3BucketRegion { get; set; }
        
        private static string AppSettingsDirectory
        {
            get
            {
                var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var appPathMatcher = new Regex(@"(?<=file:\\?)[^\\].+");
                return appPathMatcher.Match(exePath).Value;
            }
        }
        
        public static AppSettings Load()
        {
            var settings = new AppSettings();
            
            new ConfigurationBuilder()
                .SetBasePath(AppSettingsDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build()
                .Bind(settings);

            ThrowIfEmptySettings(settings);

            return settings;
        }

        public static void ThrowIfEmptySettings(AppSettings settings)
        {
            var emptyMessage = "is empty – please check the appsettings.json file";
            
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
            
            if (String.IsNullOrEmpty(settings.S3BucketRegion))
            {
                throw new Exception($"S3 Bucket region {emptyMessage}");
            }
        }
    }
}