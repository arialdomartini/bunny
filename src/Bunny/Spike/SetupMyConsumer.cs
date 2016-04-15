using System;

namespace Spike
{
  public class SetupMyConsumer : ISetup
  {
    public void Setup(Channel channel, IConsumer consumer)
    {
      Console.WriteLine("  SetupConsumer: creating queues and registering consumer");
    }
  }
}