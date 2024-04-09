using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace FileUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {

                if (Request.Body == null)
                {
                    return BadRequest("No file uploaded.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    Request.Body.CopyTo(memoryStream);

                    // Specify the directory for saving uploaded files
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                    Directory.CreateDirectory(uploadDir);
                    var uploadedFilePath = Path.Combine(uploadDir, "uploadedFile" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".zip");

                    // Save the uploaded file
                    using (var fileStream = new FileStream(uploadedFilePath, FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }

                    // Unzip the file
                    ZipFile.ExtractToDirectory(uploadedFilePath, uploadDir, true);

                    // Overwrite the current directory files
                    var extractedFiles = Directory.EnumerateFiles(uploadDir);
                    foreach (var extractedFile in extractedFiles)
                    {
                        try
                        {

                            var fileName = Path.GetFileName(extractedFile);
                            var destinationPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                            if (System.IO.File.Exists(destinationPath))
                            {
                                System.IO.File.Delete(destinationPath);
                            }
                            System.IO.File.Copy(extractedFile, destinationPath, true);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    foreach (var extractedFile in extractedFiles)
                    {
                        try
                        {

                            var fileName = Path.GetFileName(extractedFile);
                            var destinationPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                            if (System.IO.File.Exists(destinationPath))
                            {
                                System.IO.File.Delete(destinationPath);
                            }
                            System.IO.File.Move(extractedFile, destinationPath, true);
                        }
                        catch (Exception)
                        {
                        }
                    }

                }
                return Ok("File uploaded and unzipped successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.ToString()}");
            }
        }
    }
}
