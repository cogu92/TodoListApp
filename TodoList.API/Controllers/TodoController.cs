
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoList _todoList;

    public TodoController(ITodoList todoList)
    {
        _todoList = todoList;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_todoList.GetAllItems());

    [HttpPost]
    public IActionResult Post([FromBody] TodoItemDTO dto)
    {
        var id = _todoList.GetNextId();
        _todoList.AddItem(id, dto.Title, dto.Description, dto.Category);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string description)
    {
        _todoList.UpdateItem(id, description);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _todoList.RemoveItem(id);
        return Ok();
    }

    [HttpPost("{id}/progressions")]
    public IActionResult AddProgression(int id, [FromBody] Progression p)
    {
        _todoList.RegisterProgression(id, p.Date, p.Percent);
        return Ok();
    }
}

public record TodoItemDTO(string Title, string Description, string Category);
