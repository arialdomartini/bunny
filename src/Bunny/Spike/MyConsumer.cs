using System;

namespace Spike
{
  public class MyConsumer : IConsumer
  {
    private readonly Channel _channel;

    public MyConsumer(Channel channel)
    {
      _channel = channel;
    }

    public void ConsumeMessage(string message)
    {
      Console.WriteLine("  Consumer: consuming message, using channel {0}", _channel.GetName());
      if (message == "make the consumer crash")
      {
        throw new Exception("crashed");
      }
    }

    public void OnCancel()
    {
      Console.WriteLine("Consumer: OnCancel");
    }
  }
}