using System;
using System.Diagnostics;
using System.Threading;

namespace MyWcfService.Contracts
{
    internal static class DataContractMapper
    {
        public static LinkItemV3 MapLinkItemFromV1ToV3(LinkItemV1 item)
        {
            return new LinkItemV3
            {
                Description = item.Description,
                EndDate = item.StartDate.AddDays(14),
                Id = item.Id,
                StartDate = item.StartDate,
                Title = item.Title,
                Url = item.Url
            };
        }

        public static LinkItemV1 MapLinkItemFromV3ToV1(LinkItemV3 item)
        {
            return new LinkItemV1
            {
                Description = item.Description,
                Id = item.Id,
                StartDate = item.StartDate,
                Title = item.Title,
                Url = item.Url
            };
        }

        internal static AuditedLinkItem MapLinkItemFromV3ToAudited(LinkItemV3 item)
        {
#if V1 || V2
            string userName = Thread.CurrentPrincipal.Identity.Name;
            Debug.WriteLine("MapLinkItemFromV3ToAudited: setting LastModifiedBy to " + userName);

#if V2
            DateTime date = DateTime.Now;
            Debug.WriteLine("MapLinkItemFromV3ToAudited: setting LastModifiedOn to " + date);
#endif
#endif

            return new AuditedLinkItem
            {
#if V1 || V2
                InnerItem = item,
                LastModifiedBy = userName,
#if V2
                LastModifiedOn = date
#endif
#endif
            };
        }

        internal static LinkItemV3 MapLinkItemFromAuditedToV3(AuditedLinkItem item)
        {
#if V1 || V2
            Debug.WriteLine("MapLinkItemFromAuditedToV3: LastModifiedBy is " + item.LastModifiedBy);
#if V2
            Debug.WriteLine("MapLinkItemFromAuditedToV3: LastModifiedOn is " + item.LastModifiedOn);
#endif
            return item.InnerItem;
#else
            return null;
#endif
        }
    }
}