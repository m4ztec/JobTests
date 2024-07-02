# JobTests

## Folder Sync in C#
synchronizes two folders: source and replica. The program should maintain a full, identical copy of source
folder at replica folder.

Usage: FolderSync [--help] [--version] source-path target-path interval log-file-path

# Note
This is a "quick and dirty solution" a more clean solution would using proper logging (for example through DI), hashing for file/directory comparison and better error handling.