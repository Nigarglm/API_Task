using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Task.Controllers
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

        [HttpGet]
        public async Task<IActionResult> Get(int page,int take=3)
        {
            List<Category> categories = await _context.Categories.AsNoTracking().Skip((page-1)*take).Take(take).ToListAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id<=0) return StatusCode(StatusCodes.Status400BadRequest); 

            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if(category == null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category existed = await _context.Categories.FirstOrDefaultAsync(c=>c.Id== id);
            if(existed==null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Name= name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category existed = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Categories.Remove(existed);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
