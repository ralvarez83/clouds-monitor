using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Wrappers;
using SystemAdministrator.LastBackups.Domain;
using Shared.Domain.Criteria;
using Shared.Domain.ValueObjects;

namespace SystemAdministrationTest.Backups.Infraestructure.InMemoryDB
{
    public class InMemoryBackupsRepository(ApplicationDbContext appDBContext) : LastBackupsRepository
    {
        private ApplicationDbContext _appDBContext = appDBContext;

        public Task<Backup?> GetById(MachineId id)
        {
            throw new NotImplementedException();
        }

        public void Save(Backup backup)
        {
            BackupDto backupDto = BackupDtoWrapper.FromDomain(backup);
            _appDBContext.Add(backupDto);
            _appDBContext.SaveChanges();
        }

        public Task<ImmutableList<Backup>> Search(Criteria criteria)
        {
            throw new NotImplementedException();
        }

        public async Task<ImmutableList<Backup>> SearchByCriteria(Criteria criteria)
        {
            return await Task.Run(() => { return _appDBContext.Backups.Select(BackupWrapper.FromDto).ToImmutableList(); });
        }
    }
}