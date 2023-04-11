namespace Datacom.TestAutomation.Common.Extensions
{
    public static class FileExtensions
    {
        public static void DeleteFile(string path)
        {
            if (path.IsNotNullOrEmpty())
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                else
                    throw new Exception("File provided does not exist.");
            }
            else
                throw new Exception("No file path provided.");
        }

        public static FileInfo GetLatestFile(string path)
        {
            FileInfo latestFile;
            if (path.IsNotNullOrEmpty())
            {
                if (Directory.Exists(path))
                {
                    var file = new DirectoryInfo(path).GetFiles().OrderByDescending(o => o.LastWriteTime).FirstOrDefault();
                    latestFile = file != null ? file : throw new Exception("No existing files found in directory provided.");
                }
                else
                    throw new Exception("Directory provided does not exist.");
            }
            else
                throw new Exception("No directory path provided.");

            return latestFile;
        }

        public static string GetLatestFileDate(string path)
        {
            return GetLatestFile(path).LastWriteTime.ToString();
        }

        public static string GetLatestFileExtension(string path)
        {
            return GetLatestFile(path).Extension;
        }

        public static string GetLatestFileName(string path)
        {
            return GetLatestFile(path).Name;
        }

        public static string GetLatestFilePath(string path)
        {
            return GetLatestFile(path).FullName;
        }

        public static bool IsFileExists(string file, string path = "")
        {
            if (path.IsNotNullOrEmpty())
            {
                if (Directory.Exists(path))
                {
                    var targetPath = Path.Combine(path, file);
                    return File.Exists(targetPath);
                }
                else
                    throw new Exception("Directory provided does not exist.");
            }
            else
            {
                return File.Exists(file);
            }
        }

        public static bool WaitFileExists(int maxWait, string file, string path = "")
        {
            int countWait = 1;
            while (countWait <= maxWait)
            {
                bool isFileExists = path.IsNullOrEmpty() ? IsFileExists(file) : IsFileExists(file, path);
                if (!isFileExists)
                {
                    countWait++;
                    Thread.Sleep(1000);
                }
                else
                    return true;
            }
            return false;
        }
    }
}