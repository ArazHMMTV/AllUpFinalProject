using Microsoft.AspNetCore.Http;

namespace Business.Extensions;

public static class FileValidator
{
    public static bool ValidateSize(this IFormFile file, int mb)
    {

        return file.Length < mb * 1028 * 1028;
    }

    public static bool ValidateType(this IFormFile file, string type = "image")
    {
        return file.ContentType.Contains(type);

    }

    public static bool CheckImage(this IFormFile file)
    {
        return file.ValidateType() && file.ValidateSize(2);

    }

    public static async Task<string> CreateImage(this IFormFile file, string path)
    {
        string filename = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf('.'));

        path = Path.Combine(path, filename);

        using (FileStream stream = new(path, FileMode.CreateNew))
        {
            await file.CopyToAsync(stream);
        }

        return filename;
    }
}
