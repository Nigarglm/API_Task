namespace API_Task.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDto>> GetAll(int page,int take);

        Task<GetCategoryDto> GetAsync(int id);
        Task CreateAsync(CreateCategoryDto categoryDto);
    }
}
