using System;
using NUnit.Framework;

namespace Spike
{
  public interface IConsumer
  {
    void ConsumeMessage(string message);
    void OnCancel();
  }

  public class Consumer : IConsumer
  {
    public void ConsumeMessage(string message)
    {
      if (message == "make the consumer crash")
      {
        throw new Exception("crashed");
      }
      Console.WriteLine("  Consumer: consuming message");
    }

    public void OnCancel()
    {
      Console.WriteLine("Consumer: OnCancel");
    }
  }

  public interface ISetup
  {
    void Setup(Channel channel, IConsumer consumer);
  }

  public class SetupConsumer : ISetup
  {
    public void Setup(Channel channel, IConsumer consumer)
    {
      Console.WriteLine("  SetupConsumer: creating queues and registering consumer");
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
      Console.WriteLine("  Decorator: consuming message");
      try
      {
        _consumer.ConsumeMessage(message);
      }
      catch (Exception)
      {
        Console.WriteLine("  Decorator: consumer crashed");
        OnCancel();
      }
    }

    public void OnCancel()
    {
      Console.WriteLine("  Consumer: OnCancel");
    }
  }

  internal class Test
  {
    [Test]
    public void use_case()
    {
      var consumer = new Consumer();
      var decorator = new Decorator(consumer);

      var setupConsumer = new SetupConsumer();
      setupConsumer.Setup(new Channel(), decorator);

      decorator.ConsumeMessage("foo");
      decorator.ConsumeMessage("make the consumer crash");
    }
  }
}