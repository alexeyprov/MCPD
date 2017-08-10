using MyWcfService.Contracts;

namespace MyWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ContentManagerServiceV3" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ContentManagerServiceV3.svc or ContentManagerServiceV3.svc.cs at the Solution Explorer and start debugging.
    public class ContentManagerServiceV3 : IContentManagerV3
    {
        void IContentManagerV3.DeleteLinkItem(LinkItemV3 item)
        {
            ContentManagerDataAccess.DeleteLinkItem(item);
        }

        LinkItemV3 IContentManagerV3.GetLinkItem(int id)
        {
            return ContentManagerDataAccess.GetLinkItem(id);
        }

        LinkItemV3 IContentManagerV3.UpdateLinkItem(LinkItemV3 item)
        {
            return ContentManagerDataAccess.UpdateLinkItem(item);
        }
    }
}
