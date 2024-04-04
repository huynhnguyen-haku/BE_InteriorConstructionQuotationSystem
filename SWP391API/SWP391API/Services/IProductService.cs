using SWP391API.DTO;

namespace SWP391API.Services
{
    public interface IProductService
    {
        public Task deleteProductById(int id);
        public Task<ProductResponse> getProductById(int id);

        public Task<List<ProductResponse>> getProducts(ProductFilterDTO productFilterDTO);

        public Task<ProductResponse> createProduct(int userId, CreateProductDTO createProductDTO);

        public Task updateProduct(int id, int userId, UpdateProductDTO updateProductDTO);


    }
}
