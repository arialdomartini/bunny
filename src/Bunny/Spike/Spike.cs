using NUnit.Framework;

namespace Spike
{
  internal class Test
  {
    [Test]
    public void use_case()
    {
      var supervisor = new Supervisor();
      var decorator = supervisor.RegisterNewDecorator(CreateMyConsumer);

      decorator.ConsumeMessage("foo");
      decorator.ConsumeMessage("make the consumer crash");
    }

    public Decorator CreateMyConsumer()
    {
      var channel = new Channel();
      var consumer = new MyConsumer(channel);
      var decorator = new Decorator(consumer, channel);
      return decorator;
    }
  }
}