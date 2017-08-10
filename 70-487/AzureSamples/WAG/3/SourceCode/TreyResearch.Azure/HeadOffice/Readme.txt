HeadOffice

What does this project do?
----------------------------------------------
This project is the Administration portal for Trey Research and it is hosted on premises.  It is a regular ASP.NET MVC 3 web application.  You will be able to perform different tasks including:
    -  Download diagnostic logs connecting to Windows Azure storage.
    -  Monitor the Audit Log, for orders over $10,000.
    -  Manage products
    -  View a list of customers

In addition, this Project opens a service host that will expose sales data through the Service Bus Relay.


Project Dependencies
---------------------------------------------
Orders.Shared for Service Bus communications


What Projects depend on this project?
---------------------------------------------
ExternalDataAnalyzer.


How to use this project
---------------------------------------------
Before you execute this project you must first configure the Service Bus namespace in the web.config file.
You can execute this website separately from the other projects in the solution.  You will be able to download Audit log messages from a Service Bus subscription created for that purpose, or download diagnostic logs from Windows Azure storage, as well as manage the “master” Products database.
