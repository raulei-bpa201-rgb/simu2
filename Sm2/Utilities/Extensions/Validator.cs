using Sm2.Utilities.Enums;

namespace Sm2.Utilities.Extensions
{
    public static class Validator
    {
        public static bool ValidateType(this IFormFile formFile, string type)
        {
            if (formFile.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static bool ValidateSize(this IFormFile formFile, int size, FileSize fileSize)
        {
            switch (fileSize)
            {
                case FileSize.KB:
                    return formFile.Length > size * 1024;
                case FileSize.MB:
                    return formFile.Length > size * 1024 * 1024;
                case FileSize.GB:
                    return formFile.Length > size * 1024 * 1024 * 1024;
            }
            return false;
        }
        public static async Task<string> CreateFile(this IFormFile formFile, params string[] roots)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string path = string.Empty;
            for (int i = 0; i < roots.Length; i++)
            {
                path = Path.Combine(path, roots[i]);
            }
            path = Path.Combine(path, fileName);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(fs);
            }
            return fileName;
        }
        public static void DeleteFile(this string fileName, params string[] roots)
        {
            string path = string.Empty;
            for (int i = 0; i < roots.Length; i++)
            {
                path = Path.Combine(path, roots[i]);
            }
            path = Path.Combine(path, fileName);
            File.Delete(path);
        }
    }
}
