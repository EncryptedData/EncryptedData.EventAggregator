using System.Threading;
using System.Threading.Tasks;
using EncryptedData.EventAggregator.ThreadHandlers;
using FluentAssertions;
using Xunit;

namespace EncryptedData.EventAggregator.Tests;

static class TestUtilities
{
    public static Task OnCallSuccess()
    {
        // If this function is ran then it succeeded
        Assert.True(true);
        return Task.CompletedTask;
    }

    public static Task OnCallIncrement(TestIntClass.Payload payload)
    {
        payload.Count++;
        return Task.CompletedTask;
    }

    public static Task OnCallCheckBackgroundThread()
    {
        Thread.CurrentThread.IsBackground.Should().BeTrue();
        return Task.CompletedTask;
    }
}

class TestEvent : Event
{}

class TestIntClass : Event<TestIntClass.Payload>
{
    public class Payload
    {
        public int Count { get; set; } = 0;
    }
}