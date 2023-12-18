using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace API_Task.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }


        public Task<ICollection<GetCategoryDto>> GetAll(int page, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip:(page-1)*take,take:take,isTracking:false).ToListAsync();

            ICollection<GetCategoryDto> categoryDtos = new List<GetCategoryDto>();
            foreach (var category in categories)
            {
                categoryDtos.Add(new GetCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }
            return categoryDtos;
        }

        public async Task<GetCategoryDto> GetAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                throw new Exception("Not found");
            }

            return new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }


        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name
            };
        }
    }
}
