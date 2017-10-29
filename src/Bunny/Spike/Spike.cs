using Autofac;
using Autofac.Core;
using NUnit.Framework;
using Spike.IoC;

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

    [Test]
    public void use_case_with_autofac()
    {
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterModule(new SpikeModule());
      var container = containerBuilder.Build();

      var supervisor = container.Resolve<ISupervisor>();


      CreateMyConsumerWithIoC(container);
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

    public Decorator CreateMyConsumerWithIoC(IContainer container)
    {
      return container.ResolveNamed<Decorator>("myconsumer");
    }
  }
}