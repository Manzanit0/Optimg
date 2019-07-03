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

            return settings;
        }

    }
}