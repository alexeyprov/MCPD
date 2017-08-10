using System.ServiceModel;

namespace MyWcfService.Contracts
{
    [ServiceContract(Name = "ContentManagerContract", Namespace = "http://alexeypr.com/Versioning/2015/11")]
    public interface IContentManagerV2 : IContentManager
    {
        [OperationContract]
        void DeleteLinkItem(LinkItemV1 item);
    }
}
