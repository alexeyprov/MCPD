using System;
using System.ServiceModel;

namespace MyWcfService.Contracts
{
    [MessageContract(IsWrapped = false)]
    public class AuditedLinkItem
    {
#if V1 || V2
        [MessageHeader(Name = "LastModifiedBy", Namespace = "http://alexeypr.com/Versioning/2015/10", MustUnderstand = true)]
        public string LastModifiedBy
        {
            get; set;
        }

        [MessageBodyMember(Name = "LinkItem", Namespace = "http://alexeypr.com/Versioning/2015/10")]
        public LinkItemV3 InnerItem
        {
            get; set;
        }
#endif

#if V2
        [MessageHeader(Name = "LastModifiedOn", Namespace = "http://alexeypr.com/Versioning/2015/11", MustUnderstand = false)]
        public DateTime LastModifiedOn
        {
            get; set;
        } 
#endif
    }
}