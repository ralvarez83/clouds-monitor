using System.Collections.Immutable;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Application.GetCloudLastBackups;
using MediatR;
using QueryDomain = Shared.Domain.Bus.Query.Query;
using Shared.Infrastructure.Bus.Query.MediatR;

namespace Clouds.LastBackups.Infrastructure.Bus.MediatR.GetCloudLast
{
  public class MediatRGetCloudLastBackupsQuery : GetCloudLastBackupsQuery, IMediatRQuery<ImmutableList<LastBackupStatusDto>, QueryDomain>
  {

    public static IRequest<ImmutableList<LastBackupStatusDto>> Wrapper(QueryDomain request) => new MediatRGetCloudLastBackupsQuery();
  }
}