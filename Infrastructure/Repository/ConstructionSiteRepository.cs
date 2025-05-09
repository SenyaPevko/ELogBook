using Domain;
using Domain.Entities.ConstructionSite;
using Domain.Entities.RecordSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.WorkIssues;
using Domain.Storage;

namespace Infrastructure.Repository;

public class ConstructionSiteRepository(
    IStorage<ConstructionSite> storage,
    IStorage<RegistrationSheet> regStorage,
    IStorage<RecordSheet> recStorage,
    IStorage<WorkIssue> issueStorage)
    : RepositoryBase<ConstructionSite, InvalidConstructionSiteReason>(storage)
{
    protected override Task ValidateCreationAsync(
        ConstructionSite entity,
        IWriteContext<InvalidConstructionSiteReason> writeContext,
        CancellationToken cancellationToken)
    {
        //todo: мб нужно изображения валидировать ._.

        // todo: мб здесь написать проверку на наличие RegistrationSheet RecordSheet WorkIssue в бд, мб они не записались типо
        // хотя их запись можно вообще в асинхрон увести, и кешировать, чтоб из базы не доставать

        return Task.CompletedTask;
    }

    protected override async Task PreprocessCreationAsync(
        ConstructionSite entity,
        IWriteContext<InvalidConstructionSiteReason> writeContext,
        CancellationToken cancellationToken)
    {
        var regSheet = new RegistrationSheet { Id = Guid.NewGuid() };
        // todo: у RecordSheet есть поле number - нужно завести счетчик в бд и просто инкрементить его - наверн
        // нужно сначала вообще понять что этот номер значит - уточнить у заказчика
        var recSheet = new RecordSheet { Id = Guid.NewGuid() };
        var workIssue = new WorkIssue { Id = Guid.NewGuid() };

        await regStorage.AddAsync(regSheet);
        await recStorage.AddAsync(recSheet);
        await issueStorage.AddAsync(workIssue);

        entity.RegistrationSheet = regSheet;
        entity.RecordSheet = recSheet;
        entity.WorkIssue = workIssue;
    }
}