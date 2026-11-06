using System;
using Xunit;
public class SimpleLinkedListTests
{
    [Fact]
    public void NewList_ShouldBeEmpty()
    {
        var list = new SimpleLinkedList<int>();
        Assert.Equal(0, list.Count);
        Assert.False(list.Contains(10));
    }
    [Fact]
    public void AddFirst_ShouldPreserveOrder()
    {
        var list = new SimpleLinkedList<int>();
        list.AddFirst(10);
        list.AddFirst(20);
        Assert.Equal(2, list.Count);
        Assert.Equal(20, list.RemoveFirst());
        Assert.Equal(10, list.RemoveFirst());
    }
    [Fact]
    public void AddLast_ShouldPreserveOrder()
    {
        var list = new SimpleLinkedList<int>();
        list.AddLast(10);
        list.AddLast(20);
        list.AddLast(30);
        Assert.Equal(10, list.RemoveFirst());
        Assert.Equal(20, list.RemoveFirst());
        Assert.Equal(30, list.RemoveFirst());
    }
    [Fact]
    public void Contains_ShouldFindElements()
    {
        var list = new SimpleLinkedList<string>();
        list.AddLast("A");
        list.AddLast("B");
        Assert.True(list.Contains("A"));
        Assert.True(list.Contains("B"));
        Assert.False(list.Contains("C"));
    }
    [Fact]

    public void RemoveFirst_ShouldUpdateCount()
    {
        var list = new SimpleLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        Assert.Equal(1, list.RemoveFirst());
        Assert.Equal(1, list.Count);
        Assert.Equal(2, list.RemoveFirst());
        Assert.Equal(0, list.Count);
    }
    [Fact]
    public void RemoveFirst_EmptyList_ShouldThrow()
    {
        var list = new SimpleLinkedList<int>();
        Assert.Throws<InvalidOperationException>(
            () => list.RemoveFirst());
    }
    [Fact]
    public void MixedOperations_ShouldPreserveOrder()
    {
        var list = new SimpleLinkedList<int>();
        list.AddFirst(2);
        list.AddFirst(1);
        list.AddLast(3);
        Assert.Equal(3, list.Count);
        Assert.Equal(1, list.RemoveFirst());
        Assert.Equal(2, list.RemoveFirst());
        Assert.Equal(3, list.RemoveFirst());
        Assert.Equal(0, list.Count);
    }
}