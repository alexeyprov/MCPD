using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Syndication;

namespace MSDNMagazine
{
[ServiceContract]
public interface IMSDNMagazineService
{
    [OperationContract]
    [WebGet(UriTemplate="/")]
    IssuesCollection GetAllIssues();
    [OperationContract]
    [WebGet(UriTemplate = "/servicedoc")]
    AtomPub10ServiceDocumentFormatter GetServiceDoc();
    [OperationContract]
    [WebGet(UriTemplate = "/feed?format={format}")]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    [ServiceKnownType(typeof(Rss20FeedFormatter))]
    SyndicationFeedFormatter GetIssuesFeed(string format);   
    [OperationContract]
    [WebGet(UriTemplate = "/{year}")]
    IssuesData GetIssuesByYear(string year);
    //remainder of interface commented for clarity
    #region Commented for clarity

    [OperationContract]
    [WebGet(UriTemplate = "/{year}/{issue}")]
    Articles GetIssue(string year, string issue);
    [OperationContract]
    [WebGet(UriTemplate = "/{year}/{issue}/{article}")]
    Article GetArticle(string year, string issue, string article);
    [OperationContract]
    [WebInvoke(UriTemplate = "/{year}/{issue}", Method = "POST")]
    Article AddArticle(string year, string issue, Article article);
    
    #endregion    
}
    [CollectionDataContract(Name="msdnMagazine",Namespace="",ItemName="year")]
    public class IssuesCollection : List<string>
    {
    }
    [DataContract(Name="issuesByYear",Namespace="")]
    public class IssuesData
    {
        [DataMember(Name="year")]
        public string Year;
        [DataMember]
        public MonthInfo Issues;
    }
    [DataContract(Name = "issue", Namespace = "")]
    public class IssueData
    {
        [DataMember(Name = "year")]
        public string Year;
        [DataMember]
        public MonthInfo Issues;
    }
    [CollectionDataContract(Name = "articles", Namespace = "")]    
    public class Articles : List<Article>
    {
    }
    [DataContract(Namespace="",Name="article")]
    public class Article
    {
        [DataMember(Name="id")]
        public string Id;
        [DataMember(Name = "author")]
        public string Author;
        [DataMember(Name = "title")]
        public string Title;
        [DataMember(Name = "content")]
        public string Content;
        [DataMember(Name = "year")]
        public string Year;
        [DataMember(Name = "issue")]
        public string Issue;
    }
    [CollectionDataContract(Name = "issues", Namespace = "",ItemName="issue")]    
    public class MonthInfo : List<string>
    {
    }

}
