using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGreatList.Models;

namespace TheGreatList.Services
{
    public interface IApiAdapter
    {
        List<Models.Task> tasks { get; set; }
        List<Category> categories { get; set; }

        public Task<List<Models.Task>> GetTasksAsync();
        public Task<List<Category>> GetCategoriesAsync();

        public System.Threading.Tasks.Task AddTaskAsync(Models.Task task);
        public System.Threading.Tasks.Task AddCategoryAsync(Category category);

        public System.Threading.Tasks.Task UpdateTaskAsync(Models.Task task);
        public System.Threading.Tasks.Task UpdateCategoryAsync(Category category);

        public System.Threading.Tasks.Task DeleteTaskAsync(int id);
        public System.Threading.Tasks.Task DeleteCategoryAsync(int id);
    }
}
