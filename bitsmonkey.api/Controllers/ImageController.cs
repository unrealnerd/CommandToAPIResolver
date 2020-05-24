using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bitsmonkey.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostImage(IFormFile file)
        {
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName, file.FileName);

            using (var stream = System.IO.File.Create(pathToSave))
            {
                await file.CopyToAsync(stream);
            }
            return Ok();
        }
    }
}