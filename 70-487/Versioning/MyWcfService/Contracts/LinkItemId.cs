using System.ServiceModel;

namespace MyWcfService.Contracts
{
    [MessageContract(IsWrapped = false)]
    public class LinkItemId
    {
        [MessageBodyMember(Name = "Id", Namespace = "http://alexeypr.com/Versioning/2015/10")]
        public int Id
        {
            get; set;
        }
    }
}