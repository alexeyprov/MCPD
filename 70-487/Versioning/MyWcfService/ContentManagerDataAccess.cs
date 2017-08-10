using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWcfService.Contracts;

namespace MyWcfService
{
    internal static class ContentManagerDataAccess
    {
        private static IDictionary<long, LinkItemV3> _items;

        static ContentManagerDataAccess()
        {
            _items = new Dictionary<long, LinkItemV3>
            {
                {
                    1,
                    new LinkItemV3
                    {
                        Id = 1,
                        Title = "Microsoft Software Developer Network",
                        Url = "https://msdn.microsoft.com",
                        StartDate = new DateTime(1998, 1, 1),
                        EndDate = new DateTime(2098, 1, 1)
                    }
                },
                {
                    2,
                    new LinkItemV3
                    {
                        Id = 2,
                        Title = "Huffington Post - Science",
                        Url = "https://www.huffingtonpost.com/science/",
                        StartDate = new DateTime(2013, 9, 1),
                        EndDate = new DateTime(2113, 9, 1),
                        Description = "Interesting stuff"
                    }
                }
            };
        }

        public static LinkItemV3 GetLinkItem(int id)
        {
            return _items[id];
        }

        public static LinkItemV3 UpdateLinkItem(LinkItemV3 item)
        {
            _items[item.Id] = item;
            return item;
        }

        public static void DeleteLinkItem(LinkItemV3 item)
        {
            _items.Remove(item.Id);
        }
    }
}