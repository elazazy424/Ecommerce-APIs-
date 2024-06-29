using AutoMapper;
using Ecommerce.Api.Dtos;
using Ecommerce.Api.Helpers;
using Ecommerce.BLL.Interfaces;
using Ecommerce.BLL.Specifications;
using Ecommerce.DAL.Entites;
using Microsoft.AspNetCore.Mvc;
namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository ;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository
            ,IGenericRepository<ProductBrand> productBrandRepository
            , IGenericRepository<ProductType> productTypeRepository,
            IGenericRepository<Product> productRepo,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _productRepo = productRepo;
            _mapper = mapper;
        }
        #region get All Products
        [HttpGet("getAllProducts")]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec  = new ProductWithTypesAndBrandsSpecification(productSpecParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productSpecParams);

            var totalItems = await _productRepo.CountAsync(countSpec);

            var products = await _productRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(new Pagination<ProductDto>(productSpecParams.PageIndex,productSpecParams.PageSize,totalItems,data));
        }
        #endregion
        #region get Product By Id
        [HttpGet("getProductById/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if (product == null)
            {
                return NotFound();
            }
            return _mapper.Map<Product, ProductDto>(product);
           
        }
        #endregion
        #region get Product types
        [HttpGet("getProductTypes")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepository.GetAllAsync();
            if (productTypes == null)
            {
                return NotFound();
            }
            return Ok(productTypes);
        }
        #endregion
        #region get Product Brands
        [HttpGet("getProductBrands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productBrandRepository.GetAllAsync();
            if (productBrands == null)
            {
                return NotFound();
            }
            return Ok(productBrands);
        }
        #endregion
    }
}
