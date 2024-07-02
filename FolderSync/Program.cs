using Cocona;
using FolderSync;

CoconaApp.Run(async ([Argument] string sourcePath,
                     [Argument] string targetPath,
                     [Argument] int interval,
                     [Argument] string logFilePath) =>
{
    if (IsInputValid())
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(interval));
        while (await timer.WaitForNextTickAsync())
        {
            SynchronizeDirectory.Synchronize(sourcePath, targetPath, logFilePath);
        }
    }

    bool IsInputValid()
    {
        if (File.GetAttributes(logFilePath).HasFlag(FileAttributes.Directory))
        {
            var message = $"{DateTime.Now} - Error: Provide path for logging must be a file path";
            Console.WriteLine(message);
            return false;
        }

        if (File.GetAttributes(sourcePath).HasFlag(FileAttributes.Directory))
        {
            var message = $"{DateTime.Now} - Error: Provided source path must be a Directory path";
            Console.WriteLine(message);
            return false;
        }

        if (File.GetAttributes(targetPath).HasFlag(FileAttributes.Directory))
        {
            var message = $"{DateTime.Now} - Error: Provided target path must be a Directory path";
            Console.WriteLine(message);
            return false;
        }
        return true;
    }
});
