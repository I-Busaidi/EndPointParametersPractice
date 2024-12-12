using EndpointParametersSolution.Models;

namespace EndpointParametersSolution.Repositories
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        (int id, string name, DateTime date) DeleteProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product UpdateProduct(Product product);
    }
}