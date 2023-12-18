using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryService _service;

        public CategoryController(ICategoryRepository repository, ICategoryService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
            //IEnumerable<Category> categories = await _repository.GetAllAsync(c=>c.Id>4,"Products","Products.Color");
            //IEnumerable<Category> categories = await _repository.GetAllAsync(orderExpression:c=>c.Name,skip:(page-1)*take,take:take);

            return Ok(await _service.GetAllAsync(page,take));
        }
        [HttpGet("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id<=0) return StatusCode(StatusCodes.Status400BadRequest); 

            return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto categoryDto)
        {
            await _service.CreateAsync(categoryDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category existed = await _repository.GetByIdAsync(id);
            if(existed==null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Name= name;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category existed = await _repository.GetByIdAsync(id);
            if (existed == null) return StatusCode(StatusCodes.Status404NotFound);

            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
