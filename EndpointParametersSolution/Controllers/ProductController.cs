using EndpointParametersSolution.DTOs;
using EndpointParametersSolution.Services;
using Microsoft.AspNetCore.Mvc;

namespace EndpointParametersSolution.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductInputDTO productInputDTO)
        {
            try
            {
                var productOutputDTO = _productService.AddProduct(productInputDTO);
                return Created(string.Empty, productOutputDTO);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllProducts(int pageNumber = 1, int pageSize = 1)
        {
            try
            {
                var products = _productService.GetAllProducts()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id = -1)
        {
            try
            {
                var product = _productService.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromBody] ProductInputDTO productInputDTO, int id = -1)
        {
            try
            {
                var productOutputDTO = _productService.UpdateProduct(productInputDTO, id);
                return Ok(productOutputDTO);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id = -1)
        {
            try
            {
                var outputContent = _productService.DeleteProduct(id);
                return Ok($"Deleted Product \"{outputContent.name}\" with ID: {outputContent.id} on {outputContent.date}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
