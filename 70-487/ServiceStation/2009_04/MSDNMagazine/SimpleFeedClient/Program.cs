using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ServiceModel.Syndication;

namespace SimpleFeedClient
{
    class Program
    {
        static void Main(string[] args)
        {
string uri = "http://localhost:1355/Issues.svc/feed";
XmlReader xr = XmlReader.Create(uri);
SyndicationFeed feed = SyndicationFeed.Load(xr);
Console.WriteLine("Feed title:{0}",feed.Title.Text);
foreach (var item in feed.Items)
{
    Console.WriteLine("Item {0}",item.Title.Text);
}
        }
    }
}
