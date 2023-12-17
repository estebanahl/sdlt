using CloudinaryDotNet.Actions;

namespace sdlt.Service.Contracts;

public interface ICloudinaryHelper
{
    Task<UploadResult> UpsertPhotoAsync(IFormFile file, string wholeUrlPath);
}

