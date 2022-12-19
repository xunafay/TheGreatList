using TheGreatList.Models;
using System.Text.Json;
using System.Text;

namespace TheGreatList.Services;

public class ApiAdapter : IApiAdapter
{
    public List<Models.Task> tasks { get; set; }
    public List<Category> categories { get; set; }

    public async Task<List<Models.Task>> GetTasksAsync()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:5277/api/");
        var res = await client.GetStringAsync("tasks");
        return Deserialize<List<Models.Task>>(res);
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:5277/api/");
        var res = await client.GetStringAsync("categories");
        return Deserialize<List<Category>>(res);
    }

    public async System.Threading.Tasks.Task AddTaskAsync(Models.Task task)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:5277/api/");
        var res = await client.PostAsync("tasks", new StringContent(JsonSerializer.Serialize(task), Encoding.UTF8, "application/json"));
        res.EnsureSuccessStatusCode();
    }

    public async System.Threading.Tasks.Task AddCategoryAsync(Category category)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:5277/api/");
        var res = await client.PostAsync("categories", new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json"));
        res.EnsureSuccessStatusCode();
    }

    public async System.Threading.Tasks.Task UpdateTaskAsync(Models.Task task)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:5277/api/");
        var res = await client.PutAsync("tasks", new StringContent(JsonSerializer.Serialize(task), Encoding.UTF8, "application/json"));
        res.EnsureSuccessStatusCode();
    }

    public async System.Threading.Tasks.Task UpdateCategoryAsync(Category category)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:5277/api/");
        var res = await client.PutAsync("categories", new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json"));
        res.EnsureSuccessStatusCode();
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:5277/api/");
        var res = await client.DeleteAsync($"tasks/{id}");
        res.EnsureSuccessStatusCode();
    }

    public async System.Threading.Tasks.Task DeleteCategoryAsync(int id)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:5277/api/");
        var res = await client.DeleteAsync($"categories/{id}");
        res.EnsureSuccessStatusCode();
    }

    private T Deserialize<T>(string json)
    {
        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<T>(json, opts);
    }
}
