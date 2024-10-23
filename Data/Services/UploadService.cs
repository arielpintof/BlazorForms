namespace BlazorForms.Data.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class UploadService
{
    private readonly IConfiguration _configuration;

    public UploadService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> UploadImage(string fileName, Stream fileStream)
    {
        var cloudinary = new Cloudinary(_configuration["Cloudinary:CloudinaryUrl"])
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