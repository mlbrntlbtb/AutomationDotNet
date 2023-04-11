using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Datacom.TestAutomation.Common
{
    public class TestDataService
    {
        private static readonly Lazy<TestDataService> data = new(() => new TestDataService());

        public static TestDataService Instance
        { get { return data.Value; } }

        public IConfigurationRoot? Root { get; set; }

        public static DirectoryInfo TryGetSolutionDirectoryInfo()
        {
            var directory = new DirectoryInfo(
                null ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory!;
        }

        public TData Load<TData>(string section = "TestData") where TData : class, new()
        {
            Root = InitializeConfiguration();
            TData data = new();
            new ConfigureFromConfigurationOptions<TData>(Instance.Root!.GetSection(section)).Configure(data);

            return data;
        }

        public TData LoadFile<TData>(string fileName, string section = "", string FilePath = "") where TData : class, new()
        {
            TData data = new();

            if (fileName != "")
            {
                Root = InitializeConfiguration(fileName);
            }
            if (section == "")
            {
                new ConfigureFromConfigurationOptions<TData>(Instance.Root!).Configure(data);
            }
            else
            {
                new ConfigureFromConfigurationOptions<TData>(Instance.Root!.GetSection(section)).Configure(data);
            }

            return data;
        }

        private static IConfigurationRoot InitializeConfiguration(string fileName = "")
        {
            string? filePath = Environment.GetEnvironmentVariable("TestData");

            if (fileName != "")
            {
                string? assemblyTargetPath = TryGetSolutionDirectoryInfo().FullName!;
                string[] filesInExecutionDir = Directory.GetFiles(assemblyTargetPath, "*.*", SearchOption.AllDirectories);
                filePath = filesInExecutionDir.FirstOrDefault(x => x.EndsWith(fileName + ".json"));
            }
            else if (string.IsNullOrWhiteSpace(filePath) || (fileName == ""))
            {
                string? assemblyTargetPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
                string[] filesInExecutionDir = Directory.GetFiles(assemblyTargetPath, "*.*", SearchOption.AllDirectories);
                if(fileName == "")
                {
                    string? projectName = TryGetSolutionDirectoryInfo().Name!.Replace(".TestAutomation", "");
                    filePath = filesInExecutionDir.FirstOrDefault(x => x.EndsWith(projectName + "TestData.json"));
                }
                else
                    filePath = filesInExecutionDir.FirstOrDefault(x => x.Contains(fileName) && x.EndsWith(".json"));
            }

            filePath = string.IsNullOrWhiteSpace(filePath) ? "TestData.json" : filePath;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            IConfigurationRoot data = new ConfigurationBuilder()
                                            .AddJsonFile(filePath, false, true)
                                            .Build();

            return data;
        }
    }
}