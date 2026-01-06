using DVLD.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Api.Controllers
{
    [Route("api/File")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService ;
        public FileController()
        {
            _fileService = new FileService("ImageProfiles");
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            string FileName = await _fileService.UploadFileAsync(fileBytes, file.FileName);
            return Ok(new { FileName });
        }
        [HttpGet("Image")]
        public IActionResult GetImage(string fileName)
        {
            try
            {
                byte[] imageBytes = _fileService.GetFileBytes(fileName);
                return File(imageBytes, "image/jpeg");
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
