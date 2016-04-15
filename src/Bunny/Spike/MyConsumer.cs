using System;

namespace Spike
{
  public class MyConsumer : IConsumer
  {
    public void ConsumeMessage(string message)
    {
      Console.WriteLine("  Consumer: consuming message");
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