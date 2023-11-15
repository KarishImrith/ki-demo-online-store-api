# Demo.Libraries.FileStorage.Abstractions
## IFileService
* Task DeleteAsync(string filePath);
* Task DownloadAsync(string filePath, string fileName, Stream stream);
* Task UploadAsync(string filePath, string fileName, Stream stream);

# Demo.Libraries.FileStorage.Local
## LocalFileService : IFileService
* Local Storage implementation