namespace Shared.Domain.Bus.Event
{
  public interface Consumer
  {
    Task Consume();
  }
}