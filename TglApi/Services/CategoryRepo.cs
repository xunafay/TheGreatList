using TglApi.Models;

namespace TglApi.Services;

public interface ICategoriesRepo
{
    IEnumerable<Category> GetCategories();
    Category GetCategory(int id);
    Category AddCategory(Category category);
    Category UpdateCategory(Category category);
    void DeleteCategory(int id);
}

public class CategoriesRepo : ICategoriesRepo
{
    private readonly TglApiDbContext _dbContext;

    public CategoriesRepo(TglApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _dbContext.Categories;
    }

    public Category GetCategory(int id)
    {
        return _dbContext.Categories.Find(id);
    }

    public Category AddCategory(Category category)
    {
        _dbContext.Categories.Add(category);
        _dbContext.SaveChanges();
        return category;
    }

    public Category UpdateCategory(Category category)
    {
        _dbContext.Categories.Update(category);
        _dbContext.SaveChanges();
        return category;
    }

    public void DeleteCategory(int id)
    {
        var category = _dbContext.Categories.Find(id);
        _dbContext.Categories.Remove(category);
        _dbContext.SaveChanges();
    }
}