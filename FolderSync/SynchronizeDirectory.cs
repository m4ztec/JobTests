namespace FolderSync;

public class SynchronizeDirectory
{

    public static void Synchronize(string sourcePath, string replicaPath, string LogFilePath)
    {
        if (!File.Exists(LogFilePath))
        {
            File.Create(LogFilePath);
        }

        // Ensure the replica directory exists
        if (!Directory.Exists(replicaPath))
        {
            Directory.CreateDirectory(replicaPath);
            var message = $"Created directory: {replicaPath}";
            Console.WriteLine(message);
            File.AppendAllText(LogFilePath, message + Environment.NewLine);
        }

        // Synchronize files
        var sourceFiles = Directory.GetFiles(sourcePath);
        var replicaFiles = Directory.GetFiles(replicaPath);

        // Copy and update files from source to replica
        foreach (var sourceFile in sourceFiles)
        {
            var fileName = Path.GetFileName(sourceFile);
            var replicaFile = Path.Combine(replicaPath, fileName);

            if (!File.Exists(replicaFile) || File.GetLastWriteTime(sourceFile) > File.GetLastWriteTime(replicaFile))
            {
                File.Copy(sourceFile, replicaFile, true);
                var message = $"Copied file: {sourceFile} to {replicaFile}";
                Console.WriteLine(message);
                File.AppendAllText(LogFilePath, message + Environment.NewLine);
            }
        }

        // Delete files in replica that are not in source
        foreach (var replicaFile in replicaFiles)
        {
            var fileName = Path.GetFileName(replicaFile);
            var sourceFile = Path.Combine(sourcePath, fileName);

            if (!File.Exists(sourceFile))
            {
                File.Delete(replicaFile);
                var message = $"Deleted file: {replicaFile}";
                Console.WriteLine(message);
                File.AppendAllText(LogFilePath, message + Environment.NewLine);
            }
        }

        // Recursively synchronize subdirectories
        var sourceDirectories = Directory.GetDirectories(sourcePath);
        var replicaDirectories = Directory.GetDirectories(replicaPath);

        foreach (var sourceDirectory in sourceDirectories)
        {
            var directoryName = Path.GetFileName(sourceDirectory);
            var replicaDirectory = Path.Combine(replicaPath, directoryName);

            Synchronize(sourceDirectory, replicaDirectory, LogFilePath);
        }

        // Delete subdirectories in replica that are not in source
        foreach (var replicaDirectory in replicaDirectories)
        {
            var directoryName = Path.GetFileName(replicaDirectory);
            var sourceDirectory = Path.Combine(sourcePath, directoryName);

            if (!Directory.Exists(sourceDirectory))
            {
                Directory.Delete(replicaDirectory, true);
                var message = $"Deleted directory: {replicaDirectory}";
                Console.WriteLine(message);
                File.AppendAllText(LogFilePath, message + Environment.NewLine);
            }
        }
    }
}
