using System;
using NUnit.Framework;

namespace Spike
{
  public interface IConsumer
  {
    void ConsumeMessage(string message);
  }

  public class Consumer : IConsumer
  {
    public void ConsumeMessage(string message)
    {
      Console.WriteLine("Consumer: consuming message");
    }
  }

  public class Decorator : IConsumer
  {
    private readonly IConsumer _consumer;

    public Decorator(IConsumer consumer)
    {
      _consumer = consumer;
    }

    public void ConsumeMessage(string message)
    {
      Console.WriteLine("Decorator: consuming message");
      _consumer.ConsumeMessage(message);
    }
  }

  internal class Test
  {
    [Test]
    public void use_case()
    {
      var consumer = new Consumer();
      var decorator = new Decorator(consumer);

      decorator.ConsumeMessage("foo");
    }
  }
}