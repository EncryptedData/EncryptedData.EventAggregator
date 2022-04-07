using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace EncryptedData.EventAggregator.Tests;

public class EventAggregatorTests
{
    [Fact]
    public async Task FireOnDefaultThread()
    {
        IEventAggregator eventAggregator = new EventAggregator();

        TestEvent e = eventAggregator.GetEvent<TestEvent>();
        using (e.Subscribe(TestUtilities.OnCallSuccess, EventHandlerThread.Default))
        {
            await e.Fire();
        }
    }
    
    [Fact]
    public async Task FireOnBackgroundThread()
    {
        IEventAggregator eventAggregator = new EventAggregator();

        TestEvent e = eventAggregator.GetEvent<TestEvent>();
        using (e.Subscribe(TestUtilities.OnCallCheckBackgroundThread, EventHandlerThread.Background))
        {
            await e.Fire();
        }
    }

    [Fact]
    public async Task CheckUnsubscribeIDisposableWorks()
    {
        IEventAggregator eventAggregator = new EventAggregator();

        TestIntClass e = eventAggregator.GetEvent<TestIntClass>();
        TestIntClass.Payload payload = new();
        
        using (e.Subscribe(TestUtilities.OnCallIncrement, EventHandlerThread.Default))
        {
            await e.Fire(payload);
        }

        await e.Fire(payload);
        payload.Count.Should().Be(1);
    }

    [Fact]
    public async Task CheckManualUnsubscribeWorks()
    {
        IEventAggregator eventAggregator = new EventAggregator();

        TestIntClass e = eventAggregator.GetEvent<TestIntClass>();
        TestIntClass.Payload payload = new();
        
        var token = e.Subscribe(TestUtilities.OnCallIncrement, EventHandlerThread.Default);
        await e.Fire(payload);

        // Unsubscribe
        token.Unsubscribe();

        await e.Fire(payload);
        payload.Count.Should().Be(1);
    }
    
    [Fact]
    public async Task CheckUnsubscribeDoesntAffectOtherSubscriptions()
    {
        IEventAggregator eventAggregator = new EventAggregator();

        TestIntClass e = eventAggregator.GetEvent<TestIntClass>();
        TestIntClass.Payload payload = new();
        
        var token = e.Subscribe(TestUtilities.OnCallIncrement, EventHandlerThread.Default);
        e.Subscribe(TestUtilities.OnCallIncrement, EventHandlerThread.Default);
        await e.Fire(payload);

        // Unsubscribe the first subscriber
        token.Unsubscribe();

        // Should only be one handler right now
        await e.Fire(payload);
        payload.Count.Should().Be(3);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public async Task FireInt(int expectedCount)
    {
        IEventAggregator eventAggregator = new EventAggregator();

        TestIntClass e = eventAggregator.GetEvent<TestIntClass>();
        using (e.Subscribe(TestUtilities.OnCallIncrement, EventHandlerThread.Default))
        {
            TestIntClass.Payload payload = new();

            for (int i = 0; i < expectedCount; i++)
            {
                await e.Fire(payload);
            }

            payload.Count.Should().Be(expectedCount);
        }
    }
}