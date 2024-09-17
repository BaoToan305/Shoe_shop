using Dapper;
using shoe_shop_productAPI.DbContexts;
using shoe_shop_productAPI.Helper;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Repository.Interface;
using System.Data;

namespace shoe_shop_productAPI.Repository
{
    public class LocalImageRespository : IImageRespository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext _dbContext;
        private DapperContext _dapperContext;

        public LocalImageRespository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, DapperContext dapperContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = context;
            _dapperContext = dapperContext;
        }

        public async Task<Image> Upload(Image image, string productId)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
               $"{image.image_name}");

            //Upload Image to Local Path

            using var steam = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(steam);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.image_name}";

            image.image_path = urlFilePath;

            // add Image to Image table


            using IDbConnection connection = _dapperContext.CreateConnection();
            var check = await connection.ExecuteAsync(StoreProceductLinks.SP_UPLOAD_IMAGE,
                new 
                { 
                    imageName = image.image_name,
                    imageExtention = image.image_extention,
                    imagePath = image.image_path,
                    imageSize = image.image_size,
                    productImageId = productId
                }, 
                commandType: CommandType.StoredProcedure);
            if(check > 0)
            {
                return image;
            }
            else
            {
                return null;
            }
        }
    }
}
