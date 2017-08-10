using System;
using System.Runtime.Serialization;

namespace MyWcfService.Contracts
{
    [DataContract(Name = "LinkItem", Namespace = "http://alexeypr.com/Versioning/2015/10/")]
    public class LinkItemV1
    {
        [DataMember(IsRequired = false, Name = "Id", Order = 0)]
        public long Id
        {
            get; set;
        }

        [DataMember(IsRequired = true, Name = "Title", Order = 1)]
        public string Title
        {
            get; set;
        }

        [DataMember(IsRequired = false, Name = "Description", Order = 2)]
        public string Description
        {
            get; set;
        }

        [DataMember(IsRequired = true, Name = "Url", Order = 3)]
        public string Url
        {
            get; set;
        }

        [DataMember(IsRequired = true, Name = "StartDate", Order = 4)]
        public DateTime StartDate
        {
            get; set;
        }
    }
}