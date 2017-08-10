﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OCM.Phoenix.EWF.Shared.BusinessEntities.Documents
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DocumentParameters", Namespace="http://schemas.datacontract.org/2004/07/OCM.Phoenix.EWF.Shared.BusinessEntities.D" +
        "ocuments")]
    public partial class DocumentParameters : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Collections.Generic.Dictionary<string, string> ParametersField;
        
        private string RelativePathField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, string> Parameters
        {
            get
            {
                return this.ParametersField;
            }
            set
            {
                this.ParametersField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RelativePath
        {
            get
            {
                return this.RelativePathField;
            }
            set
            {
                this.RelativePathField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DocumentData", Namespace="http://schemas.datacontract.org/2004/07/OCM.Phoenix.EWF.Shared.BusinessEntities.D" +
        "ocuments")]
    public partial class DocumentData : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private byte[] DataField;
        
        private string MimeTypeField;
        
        private string RelativePathField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Data
        {
            get
            {
                return this.DataField;
            }
            set
            {
                this.DataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MimeType
        {
            get
            {
                return this.MimeTypeField;
            }
            set
            {
                this.MimeTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RelativePath
        {
            get
            {
                return this.RelativePathField;
            }
            set
            {
                this.RelativePathField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(Namespace="http://www.onecallmedical.com/", ConfigurationName="DocumentsService")]
public interface DocumentsService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://www.onecallmedical.com/DocumentsService/GetDocument", ReplyAction="http://www.onecallmedical.com/DocumentsService/GetDocumentResponse")]
    OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData GetDocument(string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters document);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://www.onecallmedical.com/DocumentsService/GetConvertedDocument", ReplyAction="http://www.onecallmedical.com/DocumentsService/GetConvertedDocumentResponse")]
    OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData GetConvertedDocument(string converterName, System.Collections.Generic.Dictionary<string, string> converterParameters, string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters document);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://www.onecallmedical.com/DocumentsService/GetMultipageDocument", ReplyAction="http://www.onecallmedical.com/DocumentsService/GetMultipageDocumentResponse")]
    OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData GetMultipageDocument(string converterName, System.Collections.Generic.Dictionary<string, string> converterParameters, string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters[] documents);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://www.onecallmedical.com/DocumentsService/GetDocuments", ReplyAction="http://www.onecallmedical.com/DocumentsService/GetDocumentsResponse")]
    OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData[] GetDocuments(string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters[] documents);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://www.onecallmedical.com/DocumentsService/GetConvertedDocuments", ReplyAction="http://www.onecallmedical.com/DocumentsService/GetConvertedDocumentsResponse")]
    OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData[] GetConvertedDocuments(string converterName, System.Collections.Generic.Dictionary<string, string> converterParameters, string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters[] documents);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://www.onecallmedical.com/DocumentsService/AddDocument", ReplyAction="http://www.onecallmedical.com/DocumentsService/AddDocumentResponse")]
    long AddDocument(System.IO.Stream stream);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://www.onecallmedical.com/DocumentsService/CommitBatch", ReplyAction="http://www.onecallmedical.com/DocumentsService/CommitBatchResponse")]
    string CommitBatch(long batchID, string locationName, System.Collections.Generic.Dictionary<string, object> parameters, long webUserID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://www.onecallmedical.com/DocumentsService/RollbackBatch", ReplyAction="http://www.onecallmedical.com/DocumentsService/RollbackBatchResponse")]
    void RollbackBatch(long batchID, long webUserID, string errorInfo);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface DocumentsServiceChannel : DocumentsService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class DocumentsServiceClient : System.ServiceModel.ClientBase<DocumentsService>, DocumentsService
{
    
    public DocumentsServiceClient()
    {
    }
    
    public DocumentsServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public DocumentsServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public DocumentsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public DocumentsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData GetDocument(string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters document)
    {
        return base.Channel.GetDocument(locationName, document);
    }
    
    public OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData GetConvertedDocument(string converterName, System.Collections.Generic.Dictionary<string, string> converterParameters, string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters document)
    {
        return base.Channel.GetConvertedDocument(converterName, converterParameters, locationName, document);
    }
    
    public OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData GetMultipageDocument(string converterName, System.Collections.Generic.Dictionary<string, string> converterParameters, string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters[] documents)
    {
        return base.Channel.GetMultipageDocument(converterName, converterParameters, locationName, documents);
    }
    
    public OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData[] GetDocuments(string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters[] documents)
    {
        return base.Channel.GetDocuments(locationName, documents);
    }
    
    public OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentData[] GetConvertedDocuments(string converterName, System.Collections.Generic.Dictionary<string, string> converterParameters, string locationName, OCM.Phoenix.EWF.Shared.BusinessEntities.Documents.DocumentParameters[] documents)
    {
        return base.Channel.GetConvertedDocuments(converterName, converterParameters, locationName, documents);
    }
    
    public long AddDocument(System.IO.Stream stream)
    {
        return base.Channel.AddDocument(stream);
    }
    
    public string CommitBatch(long batchID, string locationName, System.Collections.Generic.Dictionary<string, object> parameters, long webUserID)
    {
        return base.Channel.CommitBatch(batchID, locationName, parameters, webUserID);
    }
    
    public void RollbackBatch(long batchID, long webUserID, string errorInfo)
    {
        base.Channel.RollbackBatch(batchID, webUserID, errorInfo);
    }
}