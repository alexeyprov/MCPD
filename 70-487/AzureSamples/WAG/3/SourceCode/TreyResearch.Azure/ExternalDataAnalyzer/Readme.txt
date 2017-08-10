ExternalDataAnalyzer

What does this project do?
---------------------------------------
This project consumes data exposed by the HeadOffice application. 


Project Dependencies
---------------------------------------
HeadOffice


What Projects depend on this project?
---------------------------------------
None


How to use this project
---------------------------------------
    1.  Before you start this project, you must first configure the service bus namespace in the app.config file.
    2.  Execute the HeadOffice application.
    3.  Execute the ExternalDataAnalyzer project

Note: The HeadOffice application opens a service host, exposing the necessary data that will be consumed by the ExternalDataAnalyzer project.
