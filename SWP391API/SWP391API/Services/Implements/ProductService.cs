using SWP391API.DTO;
using SWP391API.Models;
using SWP391API.Repositories;
using SWP391API.Specifications;
using SWP391API.Utilities;

namespace SWP391API.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly Repository<Product> _productRepository;
        private readonly Repository<Category> _categoryRepository;
        private readonly Repository<User> _userRepository;

        public ProductService(Repository<Product> productRepository, Repository<Category> categoryRepository, Repository<User> userRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<ProductResponse> createProduct(int userId, CreateProductDTO createProductDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(createProductDTO.CategoryId);
            if (category == null)
            {
                throw new Exception(ErrorConstants.CategoryNotFound);
            }

            var user = await _userRepository.GetByIdAsync(userId); 
            if (user == null)
            {
                throw new Exception(ErrorConstants.UserNotFound);
            }

            Product product = new Product();

            product.CategoryId = createProductDTO.CategoryId;
            product.UserId = userId;
            product.Name = createProductDTO.Name;
            product.Price = createProductDTO.Price;
            product.Description = createProductDTO.Description;
            product.Size = createProductDTO.Size;
            product.ImageUrl = createProductDTO.ImageUrl;
            product.CreatedAt = DateTime.Now;
            product.Status = product.Status;
            
            product = await _productRepository.AddAsync(product);

            return new ProductResponse(product);
        }

        public async Task deleteProductById(int id)
        {
            var spec = new ProductByIdSpec(id);
            var product = await _productRepository.FirstOrDefaultAsync(spec);

            if (product == null)
            {
                throw new Exception(ErrorConstants.ProductNotFound);
            }

            if (product.QuotationDetails.Count > 0)
            {
                throw new Exception(ErrorConstants.ProductExistInQuotationDetail);
            }

            if (product.QuotationTemps.Count > 0)
            {
                throw new Exception(ErrorConstants.ProductExistInQuotationTemps);
            }

            if (product.ProductInProjects.Count > 0)
            {
                throw new Exception(ErrorConstants.ProductExistInProject);
            }

            await _productRepository.DeleteAsync(product);
        }

        public async Task<ProductResponse> getProductById(int id)
        {
            var spec = new ProductByIdSpec(id);

            var product = await _productRepository.FirstOrDefaultAsync(spec);

            if (product == null)
            {
                throw new Exception(ErrorConstants.ProductNotFound);
            }

            return new ProductResponse(product);
        }

        public async Task<List<ProductResponse>> getProducts(ProductFilterDTO productFilterDTO)
        {
            var spec = new ProductByFilterSpec(productFilterDTO);

            var products = await _productRepository.ListAsync(spec);

            return products.Select(p => new ProductResponse(p)).ToList();
        }

        public async Task updateProduct(int id, int userId, UpdateProductDTO updateProductDTO)
        {
            var spec = new ProductByIdSpec(id);
            var product = await _productRepository.FirstOrDefaultAsync(spec);

            if (product == null)
            {
                throw new Exception(ErrorConstants.ProductNotFound);
            }

            var category = await _categoryRepository.GetByIdAsync(updateProductDTO.CategoryId);
            if (category == null)
            {
                throw new Exception(ErrorConstants.CategoryNotFound);
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception(ErrorConstants.UserNotFound);
            }

            product.CategoryId = updateProductDTO.CategoryId;
            product.UserId = userId;
            product.Name = updateProductDTO.Name;
            product.Price = updateProductDTO.Price;
            product.Description = updateProductDTO.Description;
            product.Size = updateProductDTO.Size;
            product.ImageUrl = updateProductDTO.ImageUrl;
            product.Status = updateProductDTO.Status;
            product.UpdatedAt = DateTime.Now;

            await _productRepository.UpdateAsync(product);
        }
    }
}
