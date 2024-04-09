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
                var file = Request.Form.Files[0];

                // Check if file is a zip file
                if (Path.GetExtension(file.FileName).ToLower() != ".zip")
                {
                    return BadRequest("Please upload a zip file.");
                }

                // Specify the directory for saving uploaded files
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                Directory.CreateDirectory(uploadDir);
                var uploadedFilePath = Path.Combine(uploadDir, file.FileName);

                // Save the uploaded file
                using (var stream = new FileStream(uploadedFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Unzip the file
                ZipFile.ExtractToDirectory(uploadedFilePath, uploadDir);

                // Overwrite the current directory files
                var extractedFiles = Directory.EnumerateFiles(uploadDir);
                foreach (var extractedFile in extractedFiles)
                {
                    var fileName = Path.GetFileName(extractedFile);
                    var destinationPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                    if (System.IO.File.Exists(destinationPath))
                    {
                        System.IO.File.Delete(destinationPath);
                    }
                    System.IO.File.Move(extractedFile, destinationPath);
                }

                return Ok("File uploaded and unzipped successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
