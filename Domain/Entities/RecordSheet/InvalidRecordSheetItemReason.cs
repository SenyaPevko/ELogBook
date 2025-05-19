namespace Domain.Entities.RecordSheet;

public enum InvalidRecordSheetItemReason
{
    ReferenceNotFound = 1,
    RecordHasBeenSigned = 2,
    FailedNotifyUsers = 3,
}