using Autofac;

namespace Spike.IoC
{
  public class SpikeModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<Channel>().AsSelf();
      builder.RegisterType<Supervisor>().As<ISupervisor>();
      builder.RegisterType<MyConsumer>().Named<IConsumer>("myconsumer");
      builder.RegisterType<AnotherConsumer>().Named<IConsumer>("anotherconsumer");
      builder.RegisterType<SetupMyConsumer>().As<ISetup>();
      builder.Register(c =>
      {
        var consumer = c.ResolveNamed<IConsumer>("myconsumer");
        var channel = c.Resolve<Channel>();
        return new Decorator(consumer, channel);
      }).Named<Decorator>("myconsumer");
      builder.Register(c =>
      {
        var consumer = c.ResolveNamed<IConsumer>("anotherconsumer");
        var channel = c.Resolve<Channel>();
        return new Decorator(consumer, channel);
      }).Named<Decorator>("anotherconsumer");
    }
  }
}