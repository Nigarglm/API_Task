namespace API_Task.Repositories.Implementations
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppContext context):base(context)
        {

        }
    }
}
