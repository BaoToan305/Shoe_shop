using shoe_shop_productAPI.DbContexts;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Repository.Interface;

namespace shoe_shop_productAPI.Repository
{
    public class LocalImageRespository : IImageRespository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext _dbContext;

        public LocalImageRespository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = context;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
               $"{image.image_name}");

            //Upload Image to Local Path

            using var steam = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(steam);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.image_name}";

            image.image_path = urlFilePath;

            // add Image to Image table

            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }
    }
}
