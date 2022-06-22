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
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get/{id?}")]
        public ActionResult Get(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c=>c.Id==id);
            if (category==null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }


        [HttpPost("create")]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpPut("edit")]
        public IActionResult Edit(Category category)
        {
            Category existedCategory = _context.Categories.FirstOrDefault(e=>e.Id == category.Id);
            if (existedCategory==null) return NotFound();
            existedCategory.Name = category.Name;

            _context.SaveChanges();
            return Ok(category);
        }

        [HttpDelete("delete")]

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, new { id });

        }

        [HttpPatch("status/{id}")]
        public IActionResult ChangerDisplayStatus(int id, string statusStr)
        {
            Category category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            bool status;
            bool result = bool.TryParse(statusStr, out status);

            if (!result) return BadRequest();
            category.DisplayStatus = status;
            _context.SaveChanges();
            return Ok();

        }

     
    }
}
