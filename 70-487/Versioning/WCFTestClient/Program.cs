using System;
using WCFTestClient.MyService;
using System.Diagnostics;

#if V1

using WCFTestClient.ContentManagerServiceV1;
using ContentManagerClient = WCFTestClient.ContentManagerServiceV1.ContentManagerContractClient;

#elif V2

using WCFTestClient.ContentManagerServiceV2;
using ContentManagerClient = WCFTestClient.ContentManagerServiceV2.ContentManagerContractClient;

#elif V3

using WCFTestClient.ContentManagerServiceV3;
using ContentManagerClient = WCFTestClient.ContentManagerServiceV3.ContentManagerContractClient;

#endif

namespace WCFTestClient
{
    class Program
    {
        private static void Main()
        {
            SimpleTest();

            ServiceAndDataContractVersionTest();

            MessageContractVersionTest();

            Console.ReadLine();  
        }

        private static void SimpleTest()
        {
            using (MyServiceClient proxy = new MyServiceClient())
            {
                string sc = proxy.GetData(4);
                Console.WriteLine(sc);
            }
        }

        private static void ServiceAndDataContractVersionTest()
        {
            using (ContentManagerClient proxy = new ContentManagerClient())
            {
                LinkItem item = proxy.GetLinkItem(1);

                item.Description += " (updated from client)";

                LinkItem updatedItem = proxy.UpdateLinkItem(item);

                Console.WriteLine($"Item {updatedItem.Id}: {updatedItem.Description}");
            }
        }

        private static void MessageContractVersionTest()
        {
            using (AuditedContentManagerService.AuditedContentManagerContractClient proxy =
                new AuditedContentManagerService.AuditedContentManagerContractClient())
            {
                AuditedContentManagerService.LinkItem item;
                proxy.GetLinkItem(2, out item);

                item.Description += " (updated from client)";

                string user = "John Doe";
                proxy.UpdateLinkItem(ref user, ref item);

                Console.WriteLine($"Item {item.Id}: {item.Description}");
            }
        }
    }
}
