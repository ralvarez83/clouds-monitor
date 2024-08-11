using System.Collections.Immutable;
using Moq;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrationTest.Backups.Domain;
using Microsoft.EntityFrameworkCore;
using SystemAdministrationTest.Backups.Infraestructure.InMemoryDB;
using SystemAdministrator.LastBackups.Application.BackupsGetLast;
using SystemAdministrator.LastBackups.Application.Dtos.Transformation;
using Shared.Domain.Bus.Query;

namespace SystemAdministrationTest.Backups.Application
{
  public class AddNewBackups
  {
    private ApplicationDbContext _applicationDbContext;

    public AddNewBackups()
    {
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: "AddNewBackups").Options;

      _applicationDbContext = new ApplicationDbContext(options);
    }

    [Fact]
    public void NotFoundBackups()
    {
      ImmutableList<BackupDto> backupsQueryReturn = BackupsDtoFactory.BuildArrayOfBackupDtosEmpty();
      Mock<QueryBus> queryBusMok = new Mock<QueryBus>();

      queryBusMok.Setup(_ => _.Ask<ImmutableList<BackupDto>>(It.IsAny<BackupsGetLastQuery>())).ReturnsAsync(backupsQueryReturn);

      InMemoryBackupsRepository commandRepository = new InMemoryBackupsRepository(_applicationDbContext);

      // AddNewBackupsApplication addNewBackups = new AddNewBackupsApplication(commandRepository, queryBusMok.Object);

      // addNewBackups.Run();

      Assert.Empty(_applicationDbContext.Backups);

    }

    [Fact]
    public void FoundAllNewBackups()
    {
      ImmutableList<BackupDto> backupsQueryReturn = BackupsDtoFactory.BuildArrayOfBackupDtosRandom();
      Mock<QueryBus> queryBusMok = new Mock<QueryBus>();

      queryBusMok.Setup(_ => _.Ask<ImmutableList<BackupDto>>(It.IsAny<BackupsGetLastQuery>())).ReturnsAsync(backupsQueryReturn);

      InMemoryBackupsRepository commandRepository = new InMemoryBackupsRepository(_applicationDbContext);

      // AddNewBackupsApplication addNewBackups = new AddNewBackupsApplication(commandRepository, queryBusMok.Object);

      // addNewBackups.Run();

      // Assert.Equal(backupsQueryReturn.Count(), _applicationDbContext.Backups.Count());
      // foreach (BackupDto lastBackup in _applicationDbContext.Backups)
      // {
      //   Assert.Contains(lastBackup, backupsQueryReturn);
      // }
    }


    [Fact]
    public void FoundBackupsInDataBase()
    {
      ImmutableList<BackupDto> backupsQueryReturn = BackupsDtoFactory.BuildArrayOfBackupDtosRandom();

      Mock<QueryBus> queryBusMok = new Mock<QueryBus>();

      queryBusMok.Setup(_ => _.Ask<ImmutableList<BackupDto>>(It.IsAny<BackupsGetLastQuery>())).ReturnsAsync(backupsQueryReturn);

      BackupDto backupDtoInDB = backupsQueryReturn.Last();
      //InMemoryBackupsRepository commandRepository = new InMemoryBackupsRepository(_applicationDbContext);
      // commandRepository.Save(BackupDtoToBackupTransformation.Run(backupDtoInDB));

      // AddNewBackupsApplication addNewBackups = new AddNewBackupsApplication(commandRepository, queryBusMok.Object);

      // addNewBackups.Run();

      //Assert.Equal(backupsQueryReturn.Count(), _applicationDbContext.Backups.Count());
    }
  }
}