// using System.Collections.Immutable;
// using Azure.Core;
// using Azure.Identity;
// using Azure.ResourceManager;
// using Clouds.Backups.Domain;
// using Clouds.Backups.Infraestructure.Azure;
// using Clouds.Backups.Infraestructure.Azure.Configuration;
// using Microsoft.Extensions.Options;
// using CloudsTest.Backups.Infraestructure.CloudAccess.Azure.Factories;
// using Shared.Domain.Criteria.Filters;
// using Shared.Domain.Criteria;

// namespace CloudsTest.Backups.Infraestructure.CloudAccess.Azure;

// public class GetBackupsNDays
// {
//   private AzureBackupsAccess _AzureRepository;

//   private ImmutableList<LastBackupStatus> _BackupsReturn;

//   private int _Days;
//   private DateTime _EndTime;
//   private DateTime _StartTime;

//   public GetBackupsNDays()
//   {

//     List<Suscriptions> oneSuscriptionsConfiguration = SuscriptionsFactory.BuildOneSuscriptionRigthOptions();

//     IOptions<List<Suscriptions>> options = Options.Create(oneSuscriptionsConfiguration);
//     TenantsAccess tenantsAccess = new TenantsAccess(options);

//     TokenCredential cred = new DefaultAzureCredential();

//     // authenticate your client
//     ArmClient client = new ArmClient(cred);

//     _AzureRepository = new AzureBackupsAccess(client, tenantsAccess);
//     _BackupsReturn = new List<LastBackupStatus>().ToImmutableList();
//   }

//   [Fact]
//   public void SearchBackupsBetweenDates()
//   {

//     GivenHaveAccessToOneSubscriptionAndOneResourceGroup();

//     WhenAskBackupsFor30DaysBefore();

//     ThenAllBackupsShouldRuningIn30DaysOrBefore();

//   }

//   private void GivenHaveAccessToOneSubscriptionAndOneResourceGroup()
//   {
//     //It did it at Constructor Class

//   }

//   private async void WhenAskBackupsFor30DaysBefore()
//   {
//     _Days = 30;
//     _EndTime = DateTime.Today;
//     _StartTime = _EndTime.AddDays(_Days * -1);

//     Filters filters = new Filters();
//     filters.Add(new Filter(Filter.END_TIME, _EndTime.ToString(), FilterOperator.LET));
//     filters.Add(new Filter(Filter.END_TIME, _StartTime.ToString(), FilterOperator.GET));
//     Criteria criteria = new Criteria(filters);

//     _BackupsReturn = await _AzureRepository.SearchByCriteria(criteria);
//   }

//   private void ThenAllBackupsShouldRuningIn30DaysOrBefore()
//   {

//     ImmutableList<LastBackupStatus> backupsBetweenDays = _BackupsReturn.Where(backup =>
//       (null != backup.endTime && backup.endTime.Value <= _EndTime) &&
//       (null != backup.startTime && backup.startTime.Value >= _StartTime)
//     ).ToImmutableList();

//     Assert.Equal(_BackupsReturn.Count, backupsBetweenDays.Count);
//   }

// }