using System.ServiceModel;

namespace MyWcfService.Contracts
{
    [ServiceContract(Name = "AuditedContentManagerContract", Namespace = "http://alexeypr.com/Versioning/2015/10")]
    public interface IAuditedContentManager
    {
        [OperationContract]
        AuditedLinkItem GetLinkItem(LinkItemId id);

        [OperationContract]
        AuditedLinkItem UpdateLinkItem(AuditedLinkItem item);

        [OperationContract]
        void DeleteLinkItem(AuditedLinkItem item);
    }
}
