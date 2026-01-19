using System;
using System.IO;
using System.Linq;
using PZ3TodoSolution.Core;
using Xunit;

namespace PZ3TodoSolution.Core.Tests;

public class TodoListTests
{
    [Fact]
    public void Add_IncreasesCount_AndTrimsTitle()
    {
        var list = new TodoList();
        list.Add("  task  ");

        Assert.Equal(1, list.Count);
        Assert.Equal("task", list.Items.First().Title);
    }

    [Fact]
    public void Remove_ById_Works()
    {
        var list = new TodoList();
        var item = list.Add("a");

        Assert.True(list.Remove(item.Id));
        Assert.Equal(0, list.Count);
    }

    [Fact]
    public void Find_ReturnsMatches_CaseInsensitive()
    {
        var list = new TodoList();
        list.Add("Buy milk");
        list.Add("Read book");

        var found = list.Find("buy").ToList();

        Assert.Single(found);
        Assert.Equal("Buy milk", found[0].Title);
    }

    [Fact]
    public void Save_And_Load_Roundtrip_Works()
    {
        var list = new TodoList();
        var a = list.Add("Buy milk");
        list.Add("Read book");
        a.MarkDone();

        var path = Path.Combine(Path.GetTempPath(), $"pz3todo-{Guid.NewGuid()}.json");

        try
        {
            list.Save(path);
            var loaded = TodoList.Load(path);

            Assert.Equal(2, loaded.Count);
            Assert.Contains(loaded.Items, x => x.Title == "Buy milk" && x.IsDone);
        }
        finally
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
