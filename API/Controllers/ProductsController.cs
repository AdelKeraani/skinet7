
using System.Runtime.CompilerServices;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private  readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepo,
                   IGenericRepository<ProductBrand> productBrandRepo,
                   IGenericRepository<ProductType> ProductTypeRepo,IMapper mapper)
        {
            _productRepo=productRepo;
            _productBrandRepo=productBrandRepo;
            _productTypeRepo=ProductTypeRepo;
            _mapper=mapper;
        }



        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
           // var products=await _productRepo.ListAllAsync();
           var spec=new ProductsWithTypesAndBrandsSpecification();
           var products=await _productRepo.ListAsync(spec);

            // return products.Select(product=>new ProductToReturnDto
            // {
            //         Id=product.Id,
            //         Name=product.Name,
            //         Description=product.Description,
            //         PictureUrl=product.PictureUrl,
            //         Price=product.Price,
            //         ProductBrand=product.ProductBrand.Name,
            //         ProductType=product.ProductType.Name
            // }).ToList();
            return Ok(_mapper
                    .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
                //  var product= await _context.Products.FindAsync(id);
                //  var product1=await _context.Products
                //              .Where(p=>p.Id==id)
                //              .FirstOrDefaultAsync();

                //  if(product==null)
                //  {
                //     return NotFound();
                //  }
                //  return product;
                // return await _repo.GetProductByIdAsync(id);
                //return await _productRepo.GetByIdAsync(id);
           var spec=new ProductsWithTypesAndBrandsSpecification(id);
           var product= await _productRepo.GetEntityWithSpec(spec);
        //   return new ProductToReturnDto
        //   {
        //     Id=product.Id,
        //     Name=product.Name,
        //     Description=product.Description,
        //     PictureUrl=product.PictureUrl,
        //     Price=product.Price,
        //     ProductBrand=product.ProductBrand.Name,
        //     ProductType=product.ProductType.Name
        //   };
        return _mapper.Map<Product,ProductToReturnDto>(product);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetProductBrands()
        {
            //return  Ok(await _repo.GetProductBrandssAsync());
            return Ok(await _productBrandRepo.ListAllAsync());

        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>>GetProductTypes()
        {
           // return  Ok(await _repo.GetProductTypesAsync());
           return Ok(await _productTypeRepo.ListAllAsync());
        }
        
    }
}