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

        public Task<Machine?> GetById(MachineId id)
        {
            throw new NotImplementedException();
        }

        public void Save(Machine backup)
        {
            BackupDto backupDto = MachineDtoWrapper.FromDomain(backup);
            _appDBContext.Add(backupDto);
            _appDBContext.SaveChanges();
        }

        public Task<ImmutableList<Machine>> Search(Criteria criteria)
        {
            throw new NotImplementedException();
        }

        public async Task<ImmutableList<Machine>> SearchByCriteria(Criteria criteria)
        {
            return await Task.Run(() => { return _appDBContext.Backups.Select(MachineWrapper.FromDto).ToImmutableList(); });
        }
    }
}