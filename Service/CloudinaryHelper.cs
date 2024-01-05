using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using sdlt.Contracts;
using sdlt.Entities.Exceptions;

namespace sdlt.Service.Contracts;

public class CloudinaryHelper : ICloudinaryHelper
{

    private readonly Cloudinary _cloudinary;
    private readonly IConfiguration _configuration;// cuidado
    public CloudinaryHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _cloudinary = new Cloudinary(_configuration["Cloudinary:Secret"]);
        _cloudinary.Api.Secure = true;
    }

    public async Task<UploadResult> UpsertPhotoAsync(IFormFile file, string wholeUrlPath)
    {
        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParam = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),

                // se necesitan transformations
            };
            if (!string.IsNullOrEmpty(wholeUrlPath)) 
                await DeletePhotoAsync(wholeUrlPath);

            uploadResult = await _cloudinary.UploadAsync(uploadParam);
        }
        if (uploadResult.Error is not null)
            throw new CloudinaryException(uploadResult.Error.Message.ToString());

        return uploadResult;
    }

    private async Task<DeletionResult> DeletePhotoAsync(string wholeUrlPath)
    {
        var publicId = wholeUrlPath.Split('/')[7].Split(".")[0];
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result;
    }
}
