namespace Spike
{
  public interface IConsumer
  {
    void ConsumeMessage(string message);
    void OnCancel();
  }
}