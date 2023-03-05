using App.Api.Contracts.Common;
using App.Domain.Entities.Results;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    /// <summary>
    /// Information of BaseController
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// HandlerErrorResponse
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        protected IActionResult HandlerErrorResponse(Error error)
        {
            var apiError = new ErrorResponse();

            if (error.Code == App.Domain.Entities.Results.ErrorCode.NotFound)
            {
                apiError.StatusCode = 404;
                apiError.StatusPhrase = SystemConfig.NOTFOUND;
                apiError.TimeStamp = DateTime.Now;
                apiError.Errors.Add(error.Message);

                return NotFound(apiError);
            }

            apiError.StatusCode = 500;
            apiError.StatusPhrase = SystemConfig.INTERNALSERVERERROR;
            apiError.TimeStamp = DateTime.Now;
            apiError.Errors.Add(error.Message);

            return StatusCode(500, apiError);
        }

        /// <summary>
        /// Convert formfile to string
        /// </summary>
        /// <param name="file">IFormFile</param>
        /// <returns>String url image</returns>
        /// CreatedBy: ThiepTT(04/03/2023)
        protected async Task<string> ConvertFormFileToString(IFormFile file)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "bksnet-e46a7-firebase-adminsdk-7bnab-8d01c129ca.json");

            if (FirebaseApp.DefaultInstance == null)
            {
                var firebaseConfig = new AppOptions
                {
                    Credential = GoogleCredential.FromFile("bksnet-e46a7-firebase-adminsdk-7bnab-8d01c129ca.json"),
                };

                FirebaseApp.Create(firebaseConfig);
            }

            var storageClient = StorageClient.Create();

            var objectName = "images/" + Guid.NewGuid().ToString() + ".jpg";
            var options = new UploadObjectOptions
            {
                PredefinedAcl = PredefinedObjectAcl.PublicRead,
            };

            var uploadResult = await storageClient.UploadObjectAsync(
               bucket: "bksnet-e46a7.appspot.com",
               objectName: objectName,
               contentType: "image/jpeg",
               source: file.OpenReadStream(),
               options: options);

            // check uploadResult is null
            if (uploadResult is null)
            {
                return "";
            }

            var imageUrl = $"https://storage.googleapis.com/bksnet-e46a7.appspot.com/{objectName}";

            return imageUrl;
        }
    }
}