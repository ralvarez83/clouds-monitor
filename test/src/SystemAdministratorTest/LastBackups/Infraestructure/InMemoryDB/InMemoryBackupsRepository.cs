using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Transformation;
using SystemAdministrator.LastBackups.Domain;
using Shared.Domain.Criteria;

namespace SystemAdministrationTest.Backups.Infraestructure.InMemoryDB
{
    public class InMemoryBackupsRepository(ApplicationDbContext appDBContext) : LastBackupsRepository
    {
        private ApplicationDbContext _appDBContext = appDBContext;
        public void Save(Backup backup)
        {
            BackupDto backupDto = BackupToBackupDTOTransformation.Run(backup);
            _appDBContext.Add(backupDto);
            _appDBContext.SaveChanges();
        }

        public async Task<ImmutableList<Backup>> SearchByCriteria(Criteria criteria)
        {
            return await Task.Run(() => { return _appDBContext.Backups.Select(BackupDtoToBackupTransformation.Run).ToImmutableList(); });
        }
    }
}