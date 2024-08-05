using Microsoft.EntityFrameworkCore;
using Xunit.Gherkin.Quick;

namespace CloudBackupsRecoveryTest.CreateBackup
{
  [FeatureFile("./CreateBackup/CreateBackup.feature")]
  public sealed class CreateBackup : Feature
  {
    // private ApplicationDbContext _applicationDbContext;

    public CreateBackup(){
      // var options = new DbContextOptionsBuilder<ApplicationDbContext>()
      //               .UseInMemoryDatabase(databaseName: "EmptyBackups").Options;  

      // _applicationDbContext = new ApplicationDbContext(options);

      
    }

    [Given(@"the database is empty")]
    public void DataBaseIsEmpty (){
      // Assert.Empty(_applicationDbContext.Backups);
    }

    [When(@"I ask for new Backups to the Cloud")]
    public void AskToCloudForBackups(){
      
    }

    [Then(@"the database has Backups")]
    public void DatabaseHasBackups(){
      // Assert.NotEmpty(_applicationDbContext.Backups);
    }


    
  }
}