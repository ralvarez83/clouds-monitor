using System.Collections.Immutable;
using Clouds.LastBackups.Infrastructure.Repository.MongoDB;
using CloudsTest.LastBackups.Domain;
using CriteriaDomian = Shared.Domain.Criteria.Criteria;
using Shared.Domain.Criteria.Filters;
using Shared.Domain.ValueObjects;

namespace CloudsTest.LastBackups.Infrastructure.MongoDB
{
  public class SearchBackupByCriteriaShould : CloudMongoDBUnitCase
  {
    [Fact]
    public async void EmptyReturn_When_DatabaseIsEmpty()
    {
      // Given Database is empty
      using (LastBackupsStatusContext emptyContext = GetTemporalDBContext())
      {
      }

      ImmutableList<Clouds.LastBackups.Domain.LastBackupStatus> returnList = [];
      // When search without criteria
      using (LastBackupsStatusContext whenContext = GetTemporalDBContext())
      {
        MongoDBLastBackupRepository repository = new MongoDBLastBackupRepository(whenContext);

        Filters filters = new Filters();
        CriteriaDomian criteria = new CriteriaDomian(filters);

        returnList = await repository.Search(criteria);
      }

      // Then return empty list
      Assert.Empty(returnList);
      DropDataBase();
    }

    [Fact]
    public async void ReturnAllElements_When_CriteriaIsEmpty()
    {
      // Given Database is not empty
      ImmutableList<LastBackupsStatusEntity> backupsInDB = BackupsEntityFactory.BuildArrayOfBackupEntitiesRandom();
      using (LastBackupsStatusContext emptyContext = GetTemporalDBContext())
      {
        emptyContext.LastBackupStatus.AddRange(backupsInDB);
        emptyContext.SaveChanges();
      }

      ImmutableList<Clouds.LastBackups.Domain.LastBackupStatus> returnList = [];
      // When search without criteria
      using (LastBackupsStatusContext whenContext = GetTemporalDBContext())
      {
        MongoDBLastBackupRepository repository = new MongoDBLastBackupRepository(whenContext);

        Filters filters = new Filters();
        CriteriaDomian criteria = new(filters);

        returnList = await repository.Search(criteria);
      }

      // Then return empty list
      Assert.Equal(backupsInDB.Count(), returnList.Count());
      DropDataBase();
    }

    [Fact]
    public async void ReturnBackups_WithMachineId_InCriteria()
    {
      // Given Database has LastBackups records
      ImmutableList<LastBackupsStatusEntity> backupsInDB = BackupsEntityFactory.BuildArrayOfBackupEntitiesRandom();
      using (LastBackupsStatusContext emptyContext = GetTemporalDBContext())
      {
        emptyContext.LastBackupStatus.AddRange(backupsInDB);
        emptyContext.SaveChanges();
      }

      ImmutableList<LastBackupsStatusEntity> backupsMustReturn = backupsInDB.AsParallel().Where((backup, index) => index % 2 == 0).ToImmutableList();

      ImmutableList<Clouds.LastBackups.Domain.LastBackupStatus> returnList = [];

      // When search with criteria machineId in listMachineIds
      using (LastBackupsStatusContext whenContext = GetTemporalDBContext())
      {
        MongoDBLastBackupRepository repository = new MongoDBLastBackupRepository(whenContext);
        FilterValueList listMachineIds = new();

        listMachineIds.AddRange(backupsMustReturn.AsParallel().Select(backup => backup.Id).ToList<string>());

        Filters filters = new Filters();

        Filter filterIds = new Filter(MachineId.GetName(), listMachineIds.ToString(), FilterOperator.In);
        filters.Add(filterIds);
        CriteriaDomian criteria = new CriteriaDomian(filters);

        returnList = await repository.Search(criteria);
      }

      // Then return only the backups of the MachineIds
      Assert.Equal(backupsMustReturn.Count(), returnList.Count());
      Assert.Equal(backupsMustReturn.Count(), returnList.Where(backup => backupsMustReturn.Exists(backupMustBe => backupMustBe.Id == backup.MachineId.Value.ToString())).Count());
      DropDataBase();
    }
  }
}