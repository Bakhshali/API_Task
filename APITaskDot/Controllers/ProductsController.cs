using APITaskDot.DAL;
using APITaskDot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITaskDot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get/{id?}")]
        public ActionResult Get(int id)
        {
            //Product product = _product.FirstOrDefault(p=>p.Id==id);
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product==null)
            {
                return NotFound();
            }
            return Ok(product);
        }
         
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<Product> products = await _context.Products.Where(p=>p.DisplayStatus==true).ToListAsync();
            return Ok(products);
        }

        [HttpPost("create")]

        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPut]
        public IActionResult Edit(Product product)
        {
            
            Product existedProduct = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (existedProduct == null) return NotFound();
            existedProduct.Name = product.Name;
            existedProduct.Price = product.Price;
            _context.SaveChanges();
            return Ok(product);

        }
        [HttpDelete]
        //public IActionResult Delete(int id)
        //{
            
        //    Product product = _context.Products.FirstOrDefault(x=>x.Id == id);
        //    if (product == null) return NotFound();
        //    _context.Products.Remove(product);
        //    _context.SaveChanges();
        //    return Ok(product);
        //}

        public IActionResult Delete(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK,new { id});
        }

        [HttpPatch("status/{id}")]
        public IActionResult ChangeDisplayStatus(int id,string statusStr)
        {
            Product product = _context.Products.Find(id);
            if (product == null) return NotFound();

            bool status;

            bool result = bool.TryParse(statusStr, out status);
            if (!result) return BadRequest();
            
            product.DisplayStatus = status;
            _context.SaveChanges();
            return Ok();
        }


        
    }
}
