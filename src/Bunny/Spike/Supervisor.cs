using System;

namespace Spike
{

  public interface ISupervisor
  {
    void NotifyCancel(Func<Decorator> createDecorator);
    Decorator RegisterNewDecorator(Func<Decorator> createDecorator);
  }

  class Supervisor : ISupervisor
  {
    public void NotifyCancel(Func<Decorator> createDecorator)
    {
      Console.WriteLine("  Supervisor: notified of the Decorator crash");
      var decorator = RegisterNewDecorator(createDecorator);
      decorator.ConsumeMessage("bar");

    }

    public Decorator RegisterNewDecorator(Func<Decorator> createDecorator)
    {
      Console.WriteLine("  Supervisor: registering a new Decorator");

      var decorator = createDecorator();
      decorator.RegisterSupervisor(this, createDecorator);
      var setupConsumer = new SetupMyConsumer();
      var channel = new Channel(); // sbagliato, riusare il solito!
      setupConsumer.Setup(channel, decorator);

      return decorator;
    }
  }
}