namespace Shared.Domain.Dtos
{
  public interface DomainWrapperFromDto<T, U>
  {
    public static abstract T FromDto(U dataDto);
  }
}