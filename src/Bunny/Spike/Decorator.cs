using System;

namespace Spike
{
  public class Decorator : IConsumer
  {
    private readonly IConsumer _consumer;
    private ISupervisor _supervisor;
    private Func<Decorator> _createNew;

    public Decorator(IConsumer consumer)
    {
      _consumer = consumer;
    }

    public void RegisterSupervisor(ISupervisor supervisor, Func<Decorator> createNew)
    {
      _supervisor = supervisor;
      _createNew = createNew;
    }

    public void ConsumeMessage(string message)
    {
      Console.WriteLine("  Decorator: consuming message. Forwarding it to Comsumer");
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
      Console.WriteLine("  Decorator: OnCancel. Notifying the Supervisor");
      _supervisor.NotifyCancel(_createNew);
    }
  }
}