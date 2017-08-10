Orders.Workers

What does this project do?
----------------------------------------
This project is responsible for back end processing of orders.  It is a Windows Azure Worker role that manages communication with the transport partners by posting messages to a Service Bus topic, and receiving replies through a Service Bus queue.


Project Dependencies
----------------------------------------
Orders.Shared for Service Bus communication


What Projects depend on this project?
----------------------------------------
None. 


How to use this project
----------------------------------------
When executing the Orders.Azure project, the worker role will start executing two jobs, NewOrdersJob and StatusUpdateJob.  The former is in charge of processing recently placed orders through the website, and post the necessary information to a Service Bus Topic for the transport partners to pick up.  The latter listens on the OrderStatusUpdateQueue for any reply messages from the transport partners.
