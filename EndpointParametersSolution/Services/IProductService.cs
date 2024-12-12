using EndpointParametersSolution.DTOs;

namespace EndpointParametersSolution.Services
{
    public interface IProductService
    {
        ProductOutputDTO AddProduct(ProductInputDTO productInputDTO);
        (int id, string name, DateTime date) DeleteProduct(int id);
        List<ProductOutputDTO> GetAllProducts();
        ProductOutputDTO GetProductById(int id);
        ProductOutputDTO UpdateProduct(ProductInputDTO productInputDTO, int id);
    }
}