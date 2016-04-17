using Autofac;
using NUnit.Framework;
using FluentAssertions;

namespace Spike.IoC
{
  public class SpikeModuleTest
  {
    private IContainer _container;
    private ILifetimeScope _lifescope;

    [SetUp]
    public void SetUp()
    {
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterModule(new SpikeModule());
      _container = containerBuilder.Build();
      _lifescope = _container.BeginLifetimeScope();
    }

    [TearDown]
    public void TearDown()
    {
      _lifescope.Dispose();
      _container.Dispose();
    }

    [Test]
    public void should_resolve_channel()
    {
      _lifescope.Resolve<Channel>().Should().BeOfType<Channel>();
    }

    [Test]
    public void should_resolve_isupervisor_as_supervisor()
    {
      _lifescope.Resolve<ISupervisor>().Should().BeOfType<Supervisor>();
    }

    [Test]
    public void should_resolve_isetup_as_setupmyconsumer()
    {
      _lifescope.Resolve<ISetup>().Should().BeOfType<SetupMyConsumer>();
    }

    [Test]
    public void should_resolve_myconsumer()
    {
      _lifescope.ResolveNamed<IConsumer>("myconsumer").Should().BeOfType<MyConsumer>();
    }

    [Test]
    public void should_resolve_anotherconsumer()
    {
      _lifescope.ResolveNamed<IConsumer>("anotherconsumer").Should().BeOfType<AnotherConsumer>();
    }


    [Test]
    public void should_resolve_decorator_for_myconsumer()
    {
      _lifescope.ResolveNamed<Decorator>("myconsumer").Should().BeOfType<Decorator>();
    }


    [Test]
    public void should_resolve_decorator_for_anotherconsumer()
    {
      _lifescope.ResolveNamed<Decorator>("anotherconsumer").Should().BeOfType<Decorator>();
    }
  }
}