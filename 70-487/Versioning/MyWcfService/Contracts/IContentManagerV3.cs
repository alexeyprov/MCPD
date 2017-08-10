using System.ServiceModel;

namespace MyWcfService.Contracts
{
    [ServiceContract(Name = "ContentManagerContract", Namespace = "http://alexeypr.com/Versioning/2015/12")]
    public interface IContentManagerV3
    {
        [OperationContract]
        LinkItemV3 GetLinkItem(int id);

        [OperationContract]
        LinkItemV3 UpdateLinkItem(LinkItemV3 item);

        [OperationContract]
        void DeleteLinkItem(LinkItemV3 item);
    }
}
