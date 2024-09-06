
using System.Collections.Immutable;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Application.Dtos.Transformation;
using Clouds.LastBackups.Application.GetCloudLastBackups;
using Clouds.LastBackups.Domain;
using CloudsTest.LastBackups.Domain;
using CloudsTest.LastBackups.Infrastructure;
using Moq;

namespace CloudsTest.LastBackups.Application.GetCloudLast
{
    public class GetCloudLastBackupsHandlerShould : BackupsUnitTestCase
    {
        private GetCloudLastBackupsHandler _handler;

        public GetCloudLastBackupsHandlerShould()
        {
            _handler = new GetCloudLastBackupsHandler(new GetCloudLastBackups(_cloudAccess.Object));
        }

        [Fact]
        public async Task return_same_receive()
        {
            // Given
            ImmutableList<LastBackupStatus> _backupsInCloud = BackupsFactory.BuildArrayOfBackupsRandom();
            _cloudAccess
                .Setup(_ => _.GetLast())
                .ReturnsAsync(_backupsInCloud);

            // When
            ImmutableList<LastBackupStatusDto> _backupsReturned = await _handler.Handle(new GetCloudLastBackupsQuery(), CancellationToken.None);

            // Then
            Assert.Equal(_backupsInCloud.Select(LastBackupStatusDtoWrapper.FromDomain), _backupsReturned);
        }

    }
}