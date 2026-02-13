# Timesheet

This timesheet application is based on Blazor and .NET 10

### Running the application locally

* Set the Timesheet.Web project as the startup project
* run using https ot IIS Express

## Consideration made for this application

* Timesheet entries validate that the hours entered cannot be 0 or above 9
    * This was done to create limitations on the expected amount of hours worked in a day
* Unit testing was added using NUnit and the service and repository layers were tested
* Only in memory storage was used (no database or external storage solutions were in place)