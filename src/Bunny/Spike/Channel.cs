using System;

namespace Spike
{
  public class Channel
  {
    static readonly Random Rnd = new Random();
    private readonly int _name;

    public Channel()
    {
      _name = Rnd.Next(10000);
    }

    public string GetName()
    {
      return _name.ToString();
    }
  }
}