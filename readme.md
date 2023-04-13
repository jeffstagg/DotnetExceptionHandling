# Global Exception Handling in dotnet 6
---

## Overview
When a dotnet application throws an exception, by default, it gives away a lot of information about the application, its code, and what libraries it is using in the stack trace. Bad actors can use this information against us by exploiting unpatched libraries or even our own code, so it's best to return a generic exception and error message to a client when an exception occurs.

## Implementation

### Custom ErrorDetails Object
We create a custom ErrorDetails object that we use to hold information to return to the client. We can also store information here for logging, stack trace info, correlation ID, etc.

### Custom ExceptionHandling Middleware
We create a middleware to run in the dotnet framework pipeline to catch all errors as they're thrown, log the information we need to troubleshoot the issue, then craft our ErrorDetails object and our httpContext response to send to the client.

### Example Controller
We include a controller that throws the types of exceptions that an application typically throws. We also include a custom message to follow along with the error and pass information along such as parameters, and messages stating what happened during the exception. I've included some sample verbiage with hints on which exceptions to throw/catch, and which ones to only catch at the global level, based on Microsoft's recommendations found here: https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/using-standard-exception-types

### Logging
We use Serilog for logging and implement a Console sink, then style it with the theme "Code".

## Running the Project
Open a console in the parent directory and run  
``` dotnet run ```
Hit any endpoint at our local server running at port 7085 on localhost
``` curl -sS 'GET' 'https://localhost:7085/Exception' -H 'accept: */*' ```
