using EndpointParametersSolution.Models;

namespace EndpointParametersSolution.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;
        }

        public (int id, string name, DateTime date) DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return (product.productId, product.productName, DateTime.Now);
        }
    }
}
