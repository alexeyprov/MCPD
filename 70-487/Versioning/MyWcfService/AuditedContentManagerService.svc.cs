using MyWcfService.Contracts;

namespace MyWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuditedContentManagerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AuditedContentManagerService.svc or AuditedContentManagerService.svc.cs at the Solution Explorer and start debugging.
    public class AuditedContentManagerService : IAuditedContentManager
    {
        void IAuditedContentManager.DeleteLinkItem(AuditedLinkItem item)
        {
            ContentManagerDataAccess.DeleteLinkItem(DataContractMapper.MapLinkItemFromAuditedToV3(item));
        }

        AuditedLinkItem IAuditedContentManager.GetLinkItem(LinkItemId id)
        {
            return DataContractMapper.MapLinkItemFromV3ToAudited(ContentManagerDataAccess.GetLinkItem(id.Id));
        }

        AuditedLinkItem IAuditedContentManager.UpdateLinkItem(AuditedLinkItem item)
        {
            LinkItemV3 mappedItem = DataContractMapper.MapLinkItemFromAuditedToV3(item);
            mappedItem = ContentManagerDataAccess.UpdateLinkItem(mappedItem);
            return DataContractMapper.MapLinkItemFromV3ToAudited(mappedItem);
        }
    }
}
