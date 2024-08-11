using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.BackupsGetLastNDays;
using SystemAdministrator.LastBackups.Domain;
using Moq;
using SystemAdministrationTest.Backups.Domain;
using Shared.Domain.Criteria.Filters;
using Shared.Domain.Criteria;
using SystemAdministrator.LastBackups.Domain.ValueObjects;

namespace SystemAdministrationTest.Backups.Application;

public class GetBackupsLastNDaysTest
{
  [Fact]
  public async Task SendRightCriteriaBakupsShouldReturnTheSameListAsync()
  {
    ImmutableList<Backup> backupsRepositoryReturn = BackupsFactory.BuildArrayOfBackupsRandom();
    Mock<LastBackupsRepository> cloudRepositoryMok = new Mock<LastBackupsRepository>();
    int days = 2;

    DateTime endTime = DateTime.Today.AddDays(1);
    DateTime startTime = endTime.AddDays(days * -1);

    cloudRepositoryMok.Setup(_ => _.SearchByCriteria(
      It.Is<Criteria>(criteriaReceive =>
        criteriaReceive.filters.FiltersFiled.Where(filter =>
          filter.field == BackupDate.GetName() &&
          (
            (filter.fieldOperator == FilterOperator.LessEqualThan && (DateTime.Parse(filter.value) == endTime)) ||
            (filter.fieldOperator == FilterOperator.GreaterEqualThan && (DateTime.Parse(filter.value) == startTime))
          )
        ).Count() == 2)
      )
    ).ReturnsAsync(backupsRepositoryReturn);

    BackupsGetAllLastNDays backupsGetAllLastNDays = new BackupsGetAllLastNDays(cloudRepositoryMok.Object);


    ImmutableList<Backup> backupsApplicationReturn = await backupsGetAllLastNDays.Search(days);

    Assert.Equal(backupsRepositoryReturn, backupsApplicationReturn);
  }
}