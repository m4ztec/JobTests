using Cocona;
using FolderSync;

CoconaApp.Run(async ([Argument] string sourcePath,
                     [Argument] string targetPath,
                     [Argument] int interval,
                     [Argument] string logFilePath) =>
{
    var timer = new PeriodicTimer(TimeSpan.FromSeconds(interval));

    while (await timer.WaitForNextTickAsync())
    {
        SynchronizeDirectory.Synchronize(sourcePath, targetPath, logFilePath);
    }
});