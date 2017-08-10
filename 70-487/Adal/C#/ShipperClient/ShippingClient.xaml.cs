//----------------------------------------------------------------------------------------------
//    Copyright 2012 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ShipperClient
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ShippingClient : Page
    {
        private const string RelativePath = "/api/shipment";

        private readonly BackgroundWorker _httpRequestsWorker;
        private readonly AuthenticationContext _authenticationContext;
        private HRD _hrdPage;
        private string _selectedIdp;
        private IdentityProviderDescriptor _selectedIdentityProviderDescriptor;

        public ShippingClient()
        {
            InitializeComponent();

            _httpRequestsWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            // Create an AAL AuthenticationContext object and link it to the tenant backing the ShipperService
            _authenticationContext = new AuthenticationContext(ConfigurationManager.AppSettings["Tenant"]);
        }

        private void DisplayShipment_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorLabel();

            DisplayShipmentList();
        }

        private void DisplayShipmentList()
        {
            AuthenticationResult authenticationResult = GetAccessToken();

            if (authenticationResult != null)
            {
                DisableRequestButtons();
                _httpRequestsWorker.DoWork += DisplayShipmentListAsync;
                _httpRequestsWorker.RunWorkerCompleted += DisplayShipmentListAsyncCompleted;
                _httpRequestsWorker.RunWorkerAsync(authenticationResult.CreateAuthorizationHeader());
            }
        }

        private void DisplayShipmentListAsync(object sender, DoWorkEventArgs args)
        {
            string authorizationHeader = args.Argument as string;
            // Create an OAuth2 Bearer token from the AuthenticationResult                        
            args.Result = GetShipmentList(authorizationHeader);
        }

        private void DisplayShipmentListAsyncCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            _httpRequestsWorker.DoWork -= DisplayShipmentListAsync;
            _httpRequestsWorker.RunWorkerCompleted -= DisplayShipmentListAsyncCompleted;

            if (args.Error != null)
            {
                DisplayErrorMessage(args.Error.Message);
            }
            else
            {
                ShipmentAsyncResult result = args.Result as ShipmentAsyncResult;
                if (result != null)
                {
                    ShipmentsGrid.ItemsSource = result.ShipmentList;
                    ShipmentsGrid.Items.Refresh();
                }
            }

            EnableRequestButtons();
        }

        private AuthenticationResult GetAccessToken()
        {
            AuthenticationResult authenticationResult = null;
            string resource = ConfigurationManager.AppSettings["ServiceRealm"];            

            try
            {
                if (_selectedIdentityProviderDescriptor == null)
                {
                    // Get the list of Idps
                    List<IdentityProviderDescriptor> idps = (List<IdentityProviderDescriptor>)_authenticationContext.GetProviders(resource);

                    if (idps.Count > 1)
                    {
                        // pop up a Home Realm Discovery window and let the user choose an Idp
                        ChooseIdp(idps);
                    }
                    else
                    {
                        _selectedIdentityProviderDescriptor = idps[0];
                    }
                }

                // Invoke AuthenticationContext.AcquireToken to obtain an access token to access the Shipper service. 
                // It will use a pop-up window to initiate the logon flow. 
                if (_selectedIdentityProviderDescriptor != null)
                {
                    authenticationResult = _authenticationContext.AcquireToken(resource, _selectedIdentityProviderDescriptor);
                }

                ClearErrorLabel();

                if (authenticationResult == null)
                {
                    DisplayUserNotAuthenticatedError();
                }
            }
            catch (ActiveDirectoryAuthenticationException ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += (" " + ex.InnerException.Message);
                }

                DisplayErrorMessage(message);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }

            return authenticationResult;
        }

        private void ChooseIdp(List<IdentityProviderDescriptor> idps)
        {
            List<Button> list = new List<Button>();

            // create a Button for each Idp
            foreach (IdentityProviderDescriptor idp in idps)
            {
                Button b = new Button();
                b.Content = idp.Name;
                b.Click += (s, e) =>
                {
                    _selectedIdp = (string)((ContentControl)s).Content;
                    _hrdPage.Close();
                };
                list.Add(b);
            }

            _hrdPage = new HRD();

            // Add the buttons to the Home Realm Discovery window
            _hrdPage.AddButtons(list);

            // pop up the Home Realm Discovery window
            _hrdPage.ShowDialog();

            // select the Idp based on the Button clicked by the user
            _selectedIdentityProviderDescriptor = idps.SingleOrDefault(idp => idp.Name.Equals(_selectedIdp));
        }

        private void DisplayUserNotAuthenticatedError()
        {
            CreateShipmentErrorLabel.Content = "You must be authenticated to perform this operation.";
        }

        private void ClearErrorLabel()
        {
            CreateShipmentErrorLabel.Content = string.Empty;
        }

        private void CreateShipment_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorLabel();

            // validate the fields
            if (string.IsNullOrEmpty(ItemNameText.Text))
            {
                CreateShipmentErrorLabel.Content = "Item name cannot be empty";
                return;
            }

            string quantityString = ItemQuantityText.Text;
            if (string.IsNullOrEmpty(quantityString))
            {
                CreateShipmentErrorLabel.Content = "The quantity cannot be empty";
                return;
            }

            int quantity = -1;
            int.TryParse(quantityString, out quantity);
            if (quantity <= 0)
            {
                CreateShipmentErrorLabel.Content = "The quantity must be a positive number";
                return;
            }

            try
            {
                // Create the shipment 
                CreateNewShipment(ItemNameText.Text, quantity);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error creating new shipment. " + ex.Message;
                DisplayErrorMessage(errorMessage);
            }
        }

        private void CreateNewShipment(string itemName, int quantity)
        {
            AuthenticationResult authenticationResult = GetAccessToken();

            DisableRequestButtons();
            _httpRequestsWorker.DoWork += CreateNewShipmentAsync;
            _httpRequestsWorker.RunWorkerCompleted += CreateNewShipmentAsyncCompleted;

            Shipment shipment = new Shipment()
            {
                ItemName = itemName,
                Quantity = quantity
            };

            List<object> arguments = new List<object>();
            arguments.Add(shipment);
            arguments.Add(authenticationResult.CreateAuthorizationHeader());

            _httpRequestsWorker.RunWorkerAsync(arguments);
        }

        private void CreateNewShipmentAsync(object sender, DoWorkEventArgs args)
        {
            List<object> arguments = args.Argument as List<object>;

            Shipment shipment = arguments[0] as Shipment;
            string authorizationHeader = arguments[1] as string;
                                  
            args.Result = CreateNewShipment(shipment, authorizationHeader);
        }

        private void CreateNewShipmentAsyncCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            _httpRequestsWorker.DoWork -= CreateNewShipmentAsync;
            _httpRequestsWorker.RunWorkerCompleted -= CreateNewShipmentAsyncCompleted;

            if (args.Error != null)
            {
                DisplayErrorMessage(args.Error.Message);
                EnableRequestButtons();
            }
            else
            {
                ShipmentAsyncResult result = args.Result as ShipmentAsyncResult;

                if (result != null && result.ShipmentList != null)
                {
                    // display message 
                    string successMessage = "Created! New shipment ID: " + result.ShipmentList[0].ID;

                    DisplayShipmentList();

                    ItemNameText.Clear();
                    ItemQuantityText.Clear();
                    DisplayMessage(successMessage);
                }
            }
        }

        private ShipmentAsyncResult GetShipmentList(string authorizationHeader)
        {
            return GetResponseFromService("GET", authorizationHeader);
        }

        private ShipmentAsyncResult CreateNewShipment(Shipment shipment, string authorizationHeader)
        {
            return GetResponseFromService("POST", authorizationHeader, shipment);
        }

        private ShipmentAsyncResult GetResponseFromService(string httpRequestMethod, string authorizationHeader, Shipment shipment = null)
        {
            string shipperServiceUrl = ConfigurationManager.AppSettings["TargetService"] + RelativePath;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IList<Shipment> shipmentList = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(shipperServiceUrl) as HttpWebRequest;
                request.Method = httpRequestMethod;
                request.Headers["Authorization"] = authorizationHeader;
                request.ContentType = "application/json";

                if (shipment != null)
                {
                    // serialize the shipment if available
                    string shipmentData;
                    shipmentData = serializer.Serialize(shipment);

                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(Encoding.UTF8.GetBytes(shipmentData), 0, shipmentData.Length);
                    }
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // deserialize the response 
                    shipmentList = serializer.Deserialize(
                                        new StreamReader(response.GetResponseStream()).ReadToEnd(),
                                        typeof(IList<Shipment>)) as IList<Shipment>;

                    response.Close();
                }
            }
            catch (WebException webEx)
            {
                // error during fetching data
                string errorMessage = errorMessage = webEx.Message;

                if (webEx.Response != null)
                {
                    HttpWebResponse response = (HttpWebResponse)webEx.Response;

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want to clear the user from the token cache and reauthenticate?", "Error talking to service", MessageBoxButton.OKCancel);

                        if (result == MessageBoxResult.OK)
                        {
                            // clear the cache and reauthenticate the user to get a new token
                            _authenticationContext.TokenCacheStore.Clear();
                           
                            _selectedIdentityProviderDescriptor = null;                         

                            DisplayErrorMessage("Please retry your operation now!");
                        }
                        else
                        {
                            DisplayErrorMessage(errorMessage);
                        }
                        
                        return new ShipmentAsyncResult(null);
                    }
                    else
                    {
                        errorMessage += new StreamReader(webEx.Response.GetResponseStream()).ReadToEnd();
                        throw new InvalidOperationException(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while creating a new shipment. " + ex.Message);
            }

            return new ShipmentAsyncResult(shipmentList);
        }

        private void DisplayMessage(string message)
        {
            string caption = "Shipper Client";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(message, caption, button, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        private void DisplayErrorMessage(string message)
        {
            string caption = "Error";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(message, caption, button, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        private void ItemQuantity_PreviewInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DisableRequestButtons()
        {
            DisplayShipmentButton.IsEnabled = false;
            CreateShipmentButton.IsEnabled = false;

            // change the cursor to wait
            Cursor = Cursors.Wait;
            Mouse.OverrideCursor = Cursors.Wait;
        }

        private void EnableRequestButtons()
        {
            DisplayShipmentButton.IsEnabled = true;
            CreateShipmentButton.IsEnabled = true;

            // change the cursor back to normal
            Cursor = null;
            Mouse.OverrideCursor = null;
        }
    }
}
