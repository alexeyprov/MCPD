using System;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.WebApi.Server.Tests
{
    [TestClass]
    public class ApiTests
    {
        public const string ApiUrlRoot = "http://localhost:51826/api";

        [TestMethod]
        public void GetAllCategories()
        {
            WebClient client = CreateWebClient();
            string response = client.DownloadString(ApiUrlRoot + "/categories");

            Console.Write(response);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            WebClient client = CreateWebClient();
            string response = client.DownloadString(ApiUrlRoot + "/users");

            Console.Write(response);
        }

        [TestMethod]
        public void AddNewCategory()
        {
            WebClient client = CreateWebClient();

            const string url = ApiUrlRoot + "/categories";
            const string method = "POST";
            const string newCategory =
                "{\"Name\":\"Project Red\",\"Description\":\"Tasks that belong to project Red\"}";

            client.Headers.Add("Content-Type", "application/json");
            string response = client.UploadString(url, method, newCategory);

            Console.Write(response);
        }

        private WebClient CreateWebClient()
        {
            WebClient webClient = new WebClient();

            const string creds = "jbob" + ":" + "jbob12345";
            byte[] bcreds = Encoding.ASCII.GetBytes(creds);
            string base64Creds = Convert.ToBase64String(bcreds);
            webClient.Headers.Add("Authorization", "Basic " + base64Creds); // amJvYjpqYm9iMTIzNDU=
            return webClient;
        }

    }
}
