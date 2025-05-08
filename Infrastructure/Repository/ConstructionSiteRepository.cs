using Domain;
using Domain.Entities.ConstructionSite;
using Domain.Storage;

namespace Infrastructure.Repository;

// todo: тут должен быть preprocess, который бы создавал RegistrationSheet и RecordSheet, которым уже ненужны свои команды
// хотя эта логика сейчас подразумевается в маппинге вроде как ._. - ConstructionSiteStorage
public class ConstructionSiteRepository(IStorage<ConstructionSite> storage)
    : RepositoryBase<ConstructionSite, InvalidConstructionSiteReason>(storage)
{
    protected override Task ValidateCreationAsync(
        ConstructionSite entity,
        IWriteContext<InvalidConstructionSiteReason> writeContext,
        CancellationToken cancellationToken)
    {
        //todo: мб нужно изображения валидировать ._.

        return Task.CompletedTask;
    }
}