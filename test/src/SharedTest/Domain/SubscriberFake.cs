using Shared.Domain.Bus.Event;

namespace SharedTest.Domain
{
  public class SubscriberFake : Subscriber<DomainEventFake>
  {

    public const string CREATE_EXCEPTION = "CREATE_EXCEPTION";

    public SubscriberFake() { }

    public SubscriberFake(UseCaseFake useCaseFake)
    {
    }


    public Task On(DomainEvent domainEvent)
    {
      ExecuteExceptionIfHasEnviromentVariable();

      Assert.NotNull(domainEvent);

      return Task.CompletedTask;
    }

    private void ExecuteExceptionIfHasEnviromentVariable()
    {
      string? createException = Environment.GetEnvironmentVariable(CREATE_EXCEPTION);
      if (!string.IsNullOrEmpty(createException))
        throw new Exception("SubscriberFake exception");
    }
  }
}