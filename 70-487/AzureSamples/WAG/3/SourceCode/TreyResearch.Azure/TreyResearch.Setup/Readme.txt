TreyResearch.Setup

What does this project do?
--------------------------------------

This project is responsible of setting up Service Bus and ACS for the sample.  For Service Bus it will:
    -  Crete the Neworders topic
    -  Create 3 subscriptions, maninavan, worldwide and auditloglistener
    -  Create the orderstatusupdatequeue queue
    -  It will create the service identities corresponding to the transport partners, relying parties and rules to secure Service Bus

For ACS it will:

    -  Create the identity providers (Windows Live, Google, Yahoo) and create the relying party for the Orders Website
    -  Create the Man in a Van and Worldwide service identities and corresponding rule groups that will be used to authenticate transport partners when sending messages to the orderstatusupdatequeue



Project Dependencies
--------------------------------------

Microsoft.ServiceBus.AccessControlExtensions: to perform Service Bus ACS related tasks
ACS.ServiceManagementWrapper: to perform ACS configuration related tasks.
The Treyresearch.Setup project should be executed once before running the sample for the first time, but after creating your Service Bus and ACS namespaces.


What projects depend on this project?
--------------------------------------

None.  The Treyresearch.Setup project is a standalone application, and should be executed only once by the user before running the sample for the first time.


How to use this project
--------------------------------------

1.	You must first create a Service Bus and ACS namespace. 
2.	Open app.config and replace the acsnamespace, acsusername and acspassword setting values with the corresponding values from your namespace.  Do the same for the servicebusnamespace, defaultissuer and default key settings.
3.	Run the Application.
4.	Verify by logging in to the Windows Azure Management Portal
