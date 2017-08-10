//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Tailspin.Web.Survey.Shared.Stores.AzureStorage
{
    using Microsoft.WindowsAzure.StorageClient;

    public class QuestionRow : TableServiceEntity
    {
        public string Text { get; set; }

        public string Type { get; set; }

        public string PossibleAnswers { get; set; }
    }
}