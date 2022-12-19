using Microsoft.AspNetCore.Mvc;
using TglApi.Models;
using TglApi.Services;

namespace TglApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoriesRepo _categoriesRepo;

    public CategoryController(ILogger<CategoryController> logger, ICategoriesRepo categoriesRepo)
    {
        _logger = logger;
        _categoriesRepo = categoriesRepo;
    }

    public IEnumerable<Category> Get()
    {
        return _categoriesRepo.GetCategories();
    }

    [HttpGet("{id}")]
    public Category Get(int id)
    {
        return _categoriesRepo.GetCategory(id);
    }

    [HttpPost]
    public Category Post(Category category)
    {
        return _categoriesRepo.AddCategory(category);
    }

    [HttpPut]
    public Category Put(Category category)
    {
        return _categoriesRepo.UpdateCategory(category);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _categoriesRepo.DeleteCategory(id);
    }
}
