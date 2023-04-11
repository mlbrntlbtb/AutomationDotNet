using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Datacom.TestAutomation.Common
{
    public class TestSettingsService
    {
        private static readonly Lazy<TestSettingsService> settings = new(() => new TestSettingsService());
        private TestSettingsService() => Root = InitializeConfiguration();
        public static TestSettingsService Instance { get { return settings.Value; } }
        public IConfigurationRoot Root { get; }

        public TSettings Load<TSettings>(string section = "TestSettings") where TSettings : class, new()
        {
            TSettings settings = new();
            new ConfigureFromConfigurationOptions<TSettings>(Instance.Root.GetSection(section)).Configure(settings);

            return settings;
        }

        private static IConfigurationRoot InitializeConfiguration()
        {
            string? filePath = Environment.GetEnvironmentVariable("TestSettings");

            if (string.IsNullOrWhiteSpace(filePath))
            {
                string? assemblyTargetPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
                string[] filesInExecutionDir = Directory.GetFiles(assemblyTargetPath, "*.*", SearchOption.AllDirectories);
                filePath = filesInExecutionDir.FirstOrDefault(x => x.EndsWith("appsettings.json"));
            }

            filePath = string.IsNullOrWhiteSpace(filePath) ? "appsettings.json" : filePath;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            IConfigurationRoot config = new ConfigurationBuilder()
                                            .AddJsonFile(filePath, false, true)
                                            .AddEnvironmentVariables()
                                            .Build();

            return config;
        }
    }
}
