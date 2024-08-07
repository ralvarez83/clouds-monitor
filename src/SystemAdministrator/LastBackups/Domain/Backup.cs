using SystemAdministrator.LastBackups.Domain.ValueObjects;

namespace SystemAdministrator.LastBackups.Domain;

public record Backup(BackupId id, CloudBackupId cloudBackupId, BackupName name, BackupStatus status, BackupDate? startTime, BackupDate? endTime);
