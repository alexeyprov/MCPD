TransportPartner

What does this project do?
-------------------------------------------

The TransportPartner project simulates the interaction between Trey Research and the external transport partners, who will be in charge of shipping the orders.


Project Dependencies
-------------------------------------------
Orders.Shared for Service Bus communication


What projects depend on this project?
-------------------------------------------

None.  This project can be run separately from the rest.  However, it is recommended that you execute it together with the Orders.Website project for a better understanding of the general flow when a new order is placed.


How to use this project
-------------------------------------------
    1.  Execute the TransportPartner project.  The main form will show two buttons, one for each transport partner.  
    2.  Launch the Man in a Van transport partner by pressing the button on the form.  A new form will show up with a grid displaying the orders that have been received.  Upon reception, the transport partners will automatically send a reply message to Trey Research acknowledging reception.
    3.  Launch the Worldwide transport partner by pressing the button on the form.

It is recommended that you have both transport partners executing simultaneously so you can appreciate how filtering works for Service Bus subscriptions.
