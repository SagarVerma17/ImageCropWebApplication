using CorpImage.Data;
using CorpImage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;


namespace CorpImage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageDbContext _context;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, ImageDbContext imageDbContext)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _context = imageDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult SaveCroppedImage(String fileName, IFormFile blob)
        //{
        //    try
        //    {
        //        using(var image = SixLabors.ImageSharp.Image.Load(blob.OpenReadStream()))
        //        {
        //            var uploadDir = @"Images";
        //            fileName = Guid.NewGuid().ToString() + "-" + fileName;
        //            var path = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir, fileName);
        //            image.Mutate(x => x.Resize(300, 300));
        //            image.Save(path);
        //        }
        //        return Json(new { Message = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Message = "Error" });
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> SaveCroppedImage(string fileName, IFormFile blob)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await blob.CopyToAsync(memoryStream);
                    var imageModel = new ImageModel
                    {
                        FileName = fileName,
                        ImageData = memoryStream.ToArray()
                    };

                    _context.Images.AddAsync(imageModel);
                    await _context.SaveChangesAsync();
                }

                return Json(new { Message = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error" });
            }
        }


        [HttpGet]
        public IActionResult GetImages()
        {
            var images = _context.Images.ToList();
            return Json(images);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
