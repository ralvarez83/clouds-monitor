namespace Shared.Domain.Dtos
{
  public interface DtoWrapperFromDomain<U, T>
  {
    public static abstract U FromDomain(T dataDomain);
  }

}