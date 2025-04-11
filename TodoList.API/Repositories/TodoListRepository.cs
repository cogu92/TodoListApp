
public interface ITodoListRepository
{
    int GetNextId();
    List<string> GetAllCategories();
}

public class InMemoryTodoListRepository : ITodoListRepository
{
    private int _currentId = 1;
    private List<string> _categories = new List<string> { "Work", "Personal", "Hobby" };

    public int GetNextId() => _currentId++;
    public List<string> GetAllCategories() => _categories;
}
