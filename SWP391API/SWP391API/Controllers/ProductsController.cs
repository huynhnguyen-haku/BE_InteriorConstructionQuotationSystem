using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP391API.DTO;
using SWP391API.Models;
using SWP391API.Services;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;
        private readonly IProductService _productService;
        private readonly IAuthenticateService _authenticateService;


        public ProductsController(InteriorConstructionQuotationSystemContext context, IProductService productService, IAuthenticateService authenticateService)
        {
            _context = context;
            _productService = productService;
            _authenticateService = authenticateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListProducts([FromQuery] ProductFilterDTO productFilterDTO)
        {
            try
            {
                var products = await _productService.getProducts(productFilterDTO);

                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            try
            {
                var product = await _productService.getProductById(id);

                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProduct(CreateProductDTO createProductDTO)
        {
            try
            {
                int userId = _authenticateService.getCurrentUserId();

                var productDTO = await _productService.createProduct(userId, createProductDTO);

                return Ok(productDTO);
            }catch(Exception ex)
            {
                return BadRequest(new ErrorDTO(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
        {
            try
            {
                int userId = _authenticateService.getCurrentUserId();

                await _productService.updateProduct(id, userId, updateProductDTO);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.deleteProductById(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            }

        }

        [HttpGet("GetListStyle")]
        public IActionResult GetListStyle()
        {
            List<Style> lis = _context.Styles.ToList();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(lis);
        }

        [HttpGet("GetListHomeStyle")]
        public IActionResult GetListHomeStyle()
        {
            var lis = _context.HomeStyles.ToList();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(lis);
        }

        [HttpGet("GetListConstructionStyles")]
        public IActionResult GetConstructionStyles(string TypeID)
        {
            var lis = _context.ConstructionStyles.Where(x=>x.ConstructionType == TypeID).ToList();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(lis);
        }

       
    }
}
