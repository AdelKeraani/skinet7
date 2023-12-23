
using System.Runtime.CompilerServices;
using Core.Entities;
using Core.Interfaces;
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
        private  readonly IProductRepository  _repo;
        
        public ProductsController(IProductRepository repo)
        {
            _repo=repo;
        }



        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products=await _repo.GetProductsAsync();

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
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
            return await _repo.GetProductByIdAsync(id);

          
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetProductBrands()
        {
            return  Ok(await _repo.GetProductBrandssAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>>GetProductTypes()
        {
            return  Ok(await _repo.GetProductTypesAsync());
        }
        
    }
}