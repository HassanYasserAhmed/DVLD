namespace DVLD.BusinessLogicLayer
{
    public class FileService
    {
        private readonly string _basePath;
        private readonly string _subFolder;
        public FileService(string subFolder, string? basePath = null)
        {
            if (string.IsNullOrEmpty(basePath))
            {
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                string solutionRoot = Path.GetFullPath(Path.Combine(currentDir, "..\\..\\..\\..\\"));
                _basePath = Path.Combine(solutionRoot, "DVLD.Storage");
            }
            else
            {
                _basePath = basePath;
            }

            _subFolder = string.IsNullOrEmpty(subFolder) ? "" : subFolder;

            string finalPath = string.IsNullOrEmpty(_subFolder) ? _basePath : Path.Combine(_basePath, _subFolder);
            if (!Directory.Exists(finalPath))
                Directory.CreateDirectory(finalPath);
        }

        public byte[] GetFileBytes(string fileName)
        {
            string filePath = GetFilePath(fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found", fileName);

            return File.ReadAllBytes(filePath);
        }
        private string GetFilePath(string fileName)
        {
            string safeFileName = Path.GetFileName(fileName);
            return string.IsNullOrEmpty(_subFolder) ?
                   Path.Combine(_basePath, safeFileName) :
                   Path.Combine(_basePath, _subFolder, safeFileName);
        }

        public async Task<string> UploadFileAsync(byte[] fileBytes, string fileName)
        {
            if (fileBytes == null || fileBytes.Length == 0)
                throw new ArgumentException("File is empty");

            string UniqueFileName = GenerateUniqueFileName(fileName);

            string filePath = GetFilePath(UniqueFileName);
            await File.WriteAllBytesAsync(filePath, fileBytes);

            return UniqueFileName;
        }

        public bool DeleteFile(string fileName)
        {
            string filePath = GetFilePath(fileName);

            if (!File.Exists(filePath))
                return false;

            File.Delete(filePath);
            return true;
        }
        public bool FileExists(string fileName)
        {
            string filePath = GetFilePath(fileName);
            return File.Exists(filePath);
        }

        private string GenerateUniqueFileName(string originalFileName)
        {
            if (string.IsNullOrWhiteSpace(originalFileName))
                throw new ArgumentException("File name is required");

            string extension = Path.GetExtension(originalFileName);
                                                                  
            string guid = Guid.NewGuid().ToString("N");
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

            string uniqueFileName = $"{guid}_{timestamp}{extension}";

            return uniqueFileName;
        }
    }
}
