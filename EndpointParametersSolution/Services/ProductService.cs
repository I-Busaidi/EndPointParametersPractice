using AutoMapper;
using EndpointParametersSolution.DTOs;
using EndpointParametersSolution.Models;
using EndpointParametersSolution.Repositories;

namespace EndpointParametersSolution.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public List<ProductOutputDTO> GetAllProducts()
        {
            List<Product> products = _productRepository.GetAllProducts().ToList();
            List<ProductOutputDTO> productsDTO = _mapper.Map<List<ProductOutputDTO>>(products);
            if (productsDTO == null || productsDTO.Count == 0)
            {
                throw new InvalidOperationException("No products available.");
            }
            return productsDTO;
        }

        public ProductOutputDTO GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);

            var productDTO = _mapper.Map<ProductOutputDTO>(product);
            if (productDTO == null)
            {
                throw new KeyNotFoundException("Could not find product.");
            }
            return productDTO;
        }

        public ProductOutputDTO AddProduct(ProductInputDTO productInputDTO)
        {
            if (productInputDTO == null)
            {
                throw new InvalidOperationException("Product information is empty.");
            }

            var products = _productRepository.GetAllProducts();
            if (products.Any(p => p.productName == productInputDTO.productName))
            {
                throw new InvalidOperationException("A product with this name already exists.");
            }

            if (productInputDTO.productPrice <= 0)
            {
                throw new ArgumentException("Price must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(productInputDTO.productName))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            var productIn = _mapper.Map<Product>(productInputDTO);

            var productOut = _productRepository.AddProduct(productIn);

            var productOutputDTO = _mapper.Map<ProductOutputDTO>(productOut);

            return productOutputDTO;
        }

        public ProductOutputDTO UpdateProduct(ProductInputDTO productInputDTO, int id)
        {
            if (id == -1)
            {
                throw new InvalidOperationException("No ID provided.");
            }

            if (productInputDTO == null)
            {
                throw new InvalidOperationException("Product information is empty.");
            }

            if (productInputDTO.productPrice <= 0)
            {
                throw new ArgumentException("Price must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(productInputDTO.productName))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Could not find product.");
            }

            var productToCheck = _productRepository.GetAllProducts()
                .FirstOrDefault(p => p.productName == productInputDTO.productName);
            if (productToCheck != null && productToCheck.productId != product.productId)
            {
                throw new ArgumentException("A product with this name already exists.");
            }

            var productIn = _mapper.Map<Product>(productInputDTO);
            productIn.productId = product.productId;

            var productOut = _productRepository.UpdateProduct(productIn);

            var productOutputDTO = _mapper.Map<ProductOutputDTO>(productOut);

            return productOutputDTO;
        }

        public (int id, string name, DateTime date) DeleteProduct(int id)
        {
            if (id == -1)
            {
                throw new InvalidOperationException("No ID provided.");
            }

            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Could not find product.");
            }

            return _productRepository.DeleteProduct(product);
        }
    }
}
