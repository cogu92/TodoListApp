
public interface ITodoList
{
    void AddItem(int id, string title, string description, string category);
    void UpdateItem(int id, string description);
    void RemoveItem(int id);
    void RegisterProgression(int id, DateTime dateTime, decimal percent);
    IEnumerable<TodoItem> GetAllItems();
    int GetNextId();
}

public class TodoList : ITodoList
{
    private readonly ITodoListRepository _repo;
    private readonly List<TodoItem> _items = new();

    public TodoList(ITodoListRepository repo)
    {
        _repo = repo;
    }

    public void AddItem(int id, string title, string description, string category)
    {
        if (!_repo.GetAllCategories().Contains(category))
            throw new Exception("Invalid category.");
        _items.Add(new TodoItem { Id = id, Title = title, Description = description, Category = category });
    }

    public void UpdateItem(int id, string description)
    {
        var item = _items.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Not found.");
        if (item.Progressions.Sum(p => p.Percent) > 50)
            throw new Exception("Cannot update item with more than 50% done.");
        item.Description = description;
    }

    public void RemoveItem(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Not found.");
        if (item.Progressions.Sum(p => p.Percent) > 50)
            throw new Exception("Cannot delete item with more than 50% done.");
        _items.Remove(item);
    }

    public void RegisterProgression(int id, DateTime dateTime, decimal percent)
    {
        var item = _items.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Not found.");
        if (percent <= 0 || percent >= 100)
            throw new Exception("Percent out of bounds.");

        var total = item.Progressions.Sum(p => p.Percent);
        if (total + percent > 100)
            throw new Exception("Progress exceeds 100%.");

        if (item.Progressions.Count > 0 && item.Progressions.Max(p => p.Date) >= dateTime)
            throw new Exception("Progression date not valid.");

        item.Progressions.Add(new Progression { Date = dateTime, Percent = percent });
    }

    public IEnumerable<TodoItem> GetAllItems() => _items.OrderBy(i => i.Id);

    public int GetNextId() => _repo.GetNextId();
}
