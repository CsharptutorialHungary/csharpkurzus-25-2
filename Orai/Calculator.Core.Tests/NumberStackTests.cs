using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Tests;

internal class NumberStackTests : GenericStackTests
{
    [Test]
    public void Count_StackIsEmpty_ReturnsZero()
    {
        NumberStack stack = new();

        Assert.That(stack, Has.Count.Zero);
    }

    [Test]
    public void Count_StackHasItems_ReturnsCorrectCount()
    {
        NumberStack stack = new();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        Assert.That(stack, Has.Count.EqualTo(3));
    }
}
