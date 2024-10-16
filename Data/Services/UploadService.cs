namespace BlazorForms.Data.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class UploadService
{
    public async Task<string> UploadImage(string fileName, Stream fileStream)
    {
        var cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"))
        {
            Api =
            {
                Secure = true
            }
        };
        
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileStream),
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = true
        };
        
        var uploadResult = await cloudinary.UploadAsync(uploadParams);
        
        return uploadResult.JsonObj["url"]?.ToString() ?? throw new InvalidOperationException();

    }
}