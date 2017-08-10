using MyWcfService.Contracts;

namespace MyWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ContentManagerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ContentManagerService.svc or ContentManagerService.svc.cs at the Solution Explorer and start debugging.
    public class ContentManagerService
#if V1
        : IContentManager
#elif V2
        : IContentManagerV2
#endif
    {
#if V1 || V2
        LinkItemV1 IContentManager.GetLinkItem(int id)
        {
            return DataContractMapper.MapLinkItemFromV3ToV1(ContentManagerDataAccess.GetLinkItem(id));
        }

        LinkItemV1 IContentManager.UpdateLinkItem(LinkItemV1 item)
        {
            LinkItemV3 mappedItem = DataContractMapper.MapLinkItemFromV1ToV3(item);
            mappedItem = ContentManagerDataAccess.UpdateLinkItem(mappedItem);
            return DataContractMapper.MapLinkItemFromV3ToV1(mappedItem);
        }
#endif

#if V2
        void IContentManagerV2.DeleteLinkItem(LinkItemV1 item)
        {
            ContentManagerDataAccess.DeleteLinkItem(DataContractMapper.MapLinkItemFromV1ToV3(item));
        }
#endif
    }
}
