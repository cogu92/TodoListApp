using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class TodoListTests
{
    private ITodoList CreateTodoList()
    {
        var repo = new InMemoryTodoListRepository();
        return new TodoList(repo);
    }

    [Fact]
    public void AddItem_ShouldAddTodoItem()
    {
        var list = CreateTodoList();
        var id = list.GetNextId();
        list.AddItem(id, "Test", "Test Desc", "Work");

        var items = list.GetAllItems();
        Assert.Single(items);
        Assert.Equal("Test", items.First().Title);
    }

    [Fact]
    public void RegisterProgression_ShouldAccumulatePercent()
    {
        var list = CreateTodoList();
        var id = list.GetNextId();
        list.AddItem(id, "Test", "Test Desc", "Work");
        list.RegisterProgression(id, new DateTime(2025, 1, 1), 30);
        list.RegisterProgression(id, new DateTime(2025, 1, 2), 40);

        var item = list.GetAllItems().First();
        Assert.Equal(70, item.Progressions.Sum(p => p.Percent));
    }

    [Fact]
    public void RegisterProgression_ShouldRejectInvalidPercent()
    {
        var list = CreateTodoList();
        var id = list.GetNextId();
        list.AddItem(id, "Test", "Test Desc", "Work");

        Assert.Throws<Exception>(() =>
            list.RegisterProgression(id, DateTime.Now, 0));
        Assert.Throws<Exception>(() =>
            list.RegisterProgression(id, DateTime.Now, 150));
    }

    [Fact]
    public void UpdateItem_ShouldFailIfProgressAbove50()
    {
        var list = CreateTodoList();
        var id = list.GetNextId();
        list.AddItem(id, "Test", "Test Desc", "Work");
        list.RegisterProgression(id, new DateTime(2025, 1, 1), 60);

        Assert.Throws<Exception>(() =>
            list.UpdateItem(id, "New Description"));
    }

    [Fact]
    public void RemoveItem_ShouldFailIfProgressAbove50()
    {
        var list = CreateTodoList();
        var id = list.GetNextId();
        list.AddItem(id, "Test", "Test Desc", "Work");
        list.RegisterProgression(id, new DateTime(2025, 1, 1), 80);

        Assert.Throws<Exception>(() =>
            list.RemoveItem(id));
    }

    [Fact]
    public void RegisterProgression_ShouldRejectOutOfOrderDate()
    {
        var list = CreateTodoList();
        var id = list.GetNextId();
        list.AddItem(id, "Test", "Test Desc", "Work");
        list.RegisterProgression(id, new DateTime(2025, 1, 5), 20);

        Assert.Throws<Exception>(() =>
            list.RegisterProgression(id, new DateTime(2025, 1, 3), 10));
    }
}
