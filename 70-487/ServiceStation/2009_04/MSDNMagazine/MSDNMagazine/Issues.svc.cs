using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Globalization;
using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;
using System.Web.UI;
using System.ServiceModel.Web;

namespace MSDNMagazine
{
    public class MSDNMagazineServiceType : IMSDNMagazineService
    {

        public AtomPub10ServiceDocumentFormatter GetServiceDoc()
        {
            OutgoingWebResponseContext ctx =
                WebOperationContext.Current.OutgoingResponse;
            ctx.ContentType = "application/atomsvc+xml";
            AtomPub10ServiceDocumentFormatter ret = null;
            //create the ServiceDocument type
            ServiceDocument doc =
                new ServiceDocument();
            IncomingWebRequestContext ictx =
                WebOperationContext.Current.IncomingRequest;
            //set the BaseUri to the current request URI
            doc.BaseUri =
                ictx.UriTemplateMatch.RequestUri;
            //create a Collection of resources   
            List<ResourceCollectionInfo> resources =
                 new List<ResourceCollectionInfo>();
            //creat the Blog resource
            ResourceCollectionInfo mainBlog =
                new ResourceCollectionInfo("MSDNMagazine",
                                    new Uri("feed", UriKind.Relative));
            //add the Accepts for this resource
            //remember this is the default if no accepts if present
            mainBlog.Accepts.Add("application/atom+xml;type=entry");
            resources.Add(mainBlog);
            //create the Pictures resource
            ResourceCollectionInfo mainPictures =
                new ResourceCollectionInfo("Pictures",
                                    new Uri("pictures", UriKind.Relative));
            //add the Accepts for this resource
            mainPictures.Accepts.Add("image/png");
            mainPictures.Accepts.Add("image/jpeg");
            mainPictures.Accepts.Add("image/gif");
            resources.Add(mainPictures);
            //create the Workspace
            Workspace main = new Workspace("Main", resources);
            //add the Workspace to the Service Document
            doc.Workspaces.Add(main);

            //get the formatter
            ret = doc.GetFormatter()
                as AtomPub10ServiceDocumentFormatter;
            return ret;
        }

        static int _firstYear = 2001;
        public SyndicationFeedFormatter GetFeedForYear(string year)
        {
            SyndicationFeedFormatter ret = null;
            SyndicationFeed myFeedData = new SyndicationFeed();
            myFeedData.Title = new TextSyndicationContent("MSDN Magazine feed");
            IssuesCollection issues = GetAllIssues();

            SyndicationItem sitem = null;
            List<SyndicationItem> list = new List<SyndicationItem>();
            myFeedData.Items = list;
            foreach (var item in issues)
            {
                sitem = new SyndicationItem
                {
                    Title = new TextSyndicationContent("MSDN Magazine: " + item),
                    Content = new TextSyndicationContent("Article highlights go here"),

                };
                sitem.Links.Add(SyndicationLink.CreateAlternateLink(new Uri("feeds/" + item, UriKind.Relative)));
                list.Add(sitem);
            }
            ret = new Atom10FeedFormatter(myFeedData);
            return ret;
        }
        public SyndicationFeedFormatter GetIssuesFeed(string format)
        {
            SyndicationFeedFormatter ret = null;
            SyndicationFeed myFeedData = new SyndicationFeed();
            myFeedData.Title = new TextSyndicationContent("MSDN Magazine feed");
            Articles articles = GetAllArticles();

            SyndicationItem sitem = null;
            List<SyndicationItem> list = new List<SyndicationItem>();
            myFeedData.Items = list;
            SyndicationLink altLink = null;
            foreach (var item in articles)
            {
                sitem = new SyndicationItem
                {
                    Title = new TextSyndicationContent("MSDN Magazine Article: " + item.Title),
                    Content = new XmlSyndicationContent(GenerateContent(item)),
                    PublishDate = GetPublished(item),
                    LastUpdatedTime = GetUpdated(item)
                };
                altLink = MakeLinkForArticle(item);
                sitem.Links.Add(altLink);
                list.Add(sitem);
            }
            if (format != null && format == "rss")
                ret = new Rss20FeedFormatter(myFeedData);
            else
                ret = new Atom10FeedFormatter(myFeedData);
            return ret;
        }

        private DateTimeOffset GetUpdated(Article item)
        {
            return GetPublished(item);
        }

        private DateTimeOffset GetPublished(Article item)
        {
            DateTimeOffset dt = new DateTimeOffset(new
                DateTime(
                Int32.Parse(item.Year),
                Int32.Parse(item.Issue), 2)
                );
            return dt;
        }

        private SyndicationLink MakeLinkForArticle(Article item)
        {
            Uri articleUri = new Uri(String.Format("{0}/{1}/{2}",
                                    item.Year,
                                    item.Issue,
                                    item.Id)
                                    , UriKind.Relative);

            return SyndicationLink.CreateAlternateLink(articleUri);
        }
        Articles GetAllArticles()
        {
            return _articles;
        }

        private XmlReader GenerateContent(Article item)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            HtmlTextWriter w = new HtmlTextWriter(sw);
            w.AddAttribute(HtmlTextWriterAttribute.Type, "xhtml");
            w.RenderBeginTag(HtmlTextWriterTag.Div);
            w.AddAttribute("xmlns", "http://www.w3.org/1999/xhtml");
            w.RenderBeginTag(HtmlTextWriterTag.Div);
            w.Write(item.Content);
            w.RenderEndTag();
            w.RenderEndTag();
            w.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            XmlReader r = XmlReader.Create(ms);
            return r;
        }
        public IssuesCollection GetAllIssues()
        {
            IssuesCollection allIssues = new IssuesCollection();
            int years = DateTime.Now.AddYears(1).Year - _firstYear;
            for (int i = 0; i < years; i++)
            {
                allIssues.Add((_firstYear + i).ToString());
            }
            allIssues.Sort((s1, s2) => Int32.Parse(s2).CompareTo(Int32.Parse(s1)));
            return allIssues;

        }



        public IssuesData GetIssuesByYear(string year)
        {
            IssuesData issue = new IssuesData { Year = year, Issues = new MonthInfo() };
            Calendar c = new GregorianCalendar();
            CultureInfo ci = CultureInfo.CurrentCulture;

            int mnths = c.GetMonthsInYear(Int32.Parse(year));
            for (int j = 0; j < mnths; j++)
            {
                issue.Issues.Add(ci.DateTimeFormat.MonthNames[j]);
            }
            return issue;

        }



        #region IIssues Members


        public Articles GetIssue(string year, string issue)
        {
            var ret = from a in _articles
                      where a.Year == year && a.Issue == issue
                      select a;
            Articles temp = new Articles();
            temp.AddRange(ret);
            return temp;
        }
        static Articles _articles = InitArticles();

        private static Articles InitArticles()
        {
            Articles ret = new Articles();
            ret.Add(new Article
            {
                Title = "SAAS: Connect Enterprise Apps with Hosted BizTalk Services ",
                Content = "In this article we introduce you to BizTalk Services, new technology that offers the Enterprise Service Bus features of BizTalk Server as a hosted service.",
                Author = "Jon Flanders and Aaron Skonnard",
                Id = "1",
                Year = "2008",
                Issue = "6"
            });


            ret.Add(new Article
            {
                Title = "Concurrency: Tools And Techniques to Identify Concurrency Issues",
                Content = "Efficient parallel applications aren’t born by merely running an old app on a parallel processor machine. Tuning needs to be done if you’re to gain maximum benefit.",
                Author = "Rahul V. Patil and Boby George",
                Id = "2",
                Year = "2008",
                Issue = "6"
            });

            ret.Add(new Article
            {
                Title = "Robotics: Simulating the World with Microsoft Robotics Studio",
                Content = "Microsoft Robotics Studio is not just for playing with robots. It also allows you to build service-based applications for a wide range of hardware devices.",
                Author = "Sara Morgan",
                Id = "3",
                Year = "2008",
                Issue = "6"
            });

            ret.Add(new Article
            {
                Title = "Form Filler: Build Workflows to Capture Data and Create Documents",
                Content = "Learn how to create a workflow that uses InfoPath forms and other office documents for passing data to targeted activities and for use in Office documents.",
                Author = "Rick Spiewak",
                Id = "4",
                Year = "2008",
                Issue = "6"
            });
            return ret;
        }

        public Article GetArticle(string year, string issue, string article)
        {
            Article a = null;
            a = (from arts in _articles
                 where arts.Id == article && arts.Issue == issue && arts.Year == year
                 select arts).Single();
            return a;
        }

        #endregion

        #region IIssues Members


        public Article AddArticle(string year, string issue, Article article)
        {
            var count = (from a in _articles
                         where a.Year == year && a.Issue == issue
                         select a).Count();
            article.Id = (count + 1).ToString();
            _articles.Add(article);
            return article;

        }

        #endregion


    }
}
