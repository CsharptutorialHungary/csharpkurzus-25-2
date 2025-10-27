namespace Calculator.Core.Tests;

internal class GenericStackTests
{
    [Test]
    public void Constructor_StackIsCreated_CreatesEmptyStack()
    {
        GenericStack<int> stack = new();

        Assert.That(stack, Is.Empty);
    }

    [Test]
    public void Push_StackIsEmpty_AddsSingleItem()
    {
        GenericStack<int> stack = new();

        stack.Push(42);

        Assert.That(stack, Has.One.Items);
        Assert.That(stack, Does.Contain(42));
    }

    [Test]
    public void Pop_StackHasOneItem_RemovesSingleItem()
    {
        GenericStack<int> stack = new();
        stack.Push(42);

        _ = stack.Pop();

        Assert.That(stack, Is.Empty);
    }

    [Test]
    public void Pop_StackIsEmpty_ThrowsInvalidOperationException()
    {
        GenericStack<int> stack = new();

        Assert.That(stack.Pop, Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_StackHasItems_ReturnsLastPushedItem()
    {
        GenericStack<int> stack = new();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        int result = stack.Pop();

        Assert.That(result, Is.EqualTo(3));
    }
}
