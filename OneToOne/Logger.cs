using Microsoft.Extensions.Logging;
using System;

namespace OneToOne
{
  public class Logger : ILogger
  {
    public IDisposable BeginScope<TState>(TState state)
    {
      return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
      Console.WriteLine(formatter(state, exception));
    }
  }
}
