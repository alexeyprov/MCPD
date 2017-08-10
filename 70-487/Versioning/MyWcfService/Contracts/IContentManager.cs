using System.ServiceModel;

namespace MyWcfService.Contracts
{
    [ServiceContract(Name = "ContentManagerContract", Namespace = "http://alexeypr.com/Versioning/2015/10")]
    public interface IContentManager
    {
        [OperationContract]
        LinkItemV1 GetLinkItem(int id);

        [OperationContract]
        LinkItemV1 UpdateLinkItem(LinkItemV1 item);
    }
}
