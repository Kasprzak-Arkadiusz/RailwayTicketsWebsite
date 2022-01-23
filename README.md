# RailwayTickets

## Table of contents
* [Motivation](#motivation)
* [General information](#general-information)
* [Technologies](#technologies)
* [Setup](#setup)
* [Sample views](#Sample-views)
* [Additional information](#additional)

## Motivation
This project was inspired by Jason Taylor Clean Architecture 
[GitHub repository](https://github.com/jasontaylordev/CleanArchitecture "GitHub repository").   
In addition, my goal was to learn the mediator pattern and improve my web development skills.

## General information
Project is a website created for a fictional railway company.  
It offers the following features:

- Search and filtering of train routes
- Buying tickets
- Management of purchased tickets (viewing, return)
- Logging in with external providers (Google, Microsoft)
- User password reset with sent email
- Viewing returned tickets as an employee
- Logging exception and errors to the database

## Technologies

- MSSQL
- C# 9
- .NET 5
- EF Core 5

### Used libraries and packages

- AutoMapper
- MailKit
- Serilog
- FluentValidation
- Swagger

## Setup
To set up a website, follow these steps:

1. Clone repository from github
2. Build solution
3. Provide email service configuration in appsetting.json in the WebApp project (Example configuration below)

```javascript
"EmailConfiguration": {
    "HostSmtp": "smtp.gmail.com",
    "EnableSsl": true,
    "Port": 465,
    "SenderEmail": "emailUsedForSendingEmails@gmail.com",
    "SenderEmailPassword": "api-key",
    "SenderName": "Sender name"
  }
```
4. Setup external api  
In the user secrets for the WebApp project, enter your API secrets

```javascript
{
  "GoogleAuthentication:ClientId": "Your-Google-Authentication-Id",
  "GoogleAuthentication:ClientSecret": "Your-Google-Authentication-Secret",
  "MicrosoftAuthentication:ClientId": "Your-Microsoft-Authentication-Id",
  "MicrosoftAuthentication:ClientSecret": "Your-Microsoft-Authentication-Secret"
}
```
5. Setup database by providing connectionString

```javascript
"ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS1;Database=TrainDB;Trusted_Connection=True;"
  },
```
If you want to log to database then provide also connectionString to database

```javascript
"Args": {
          "connectionString": "Server=.\\SQLEXPRESS1;Database=TrainDB;Trusted_Connection=True;",
          "sinkOptionsSection": {
            "tableName": "Logs",
            "schemaName": "EventLogging",
            "autoCreateSqlTable": true
          },
          "restrictedToMinimumLevel": "Warning"
        }
```
6. In .NET Core CLI run  
  `dotnet ef database update`  
  or in Package Manager Console run  
  `Update-Database`
7. Start program

## Sample views

![](/ReadmeImages/Logging-in.png)
![](/ReadmeImages/Find-routes.png)
![](/ReadmeImages/User-tickets.png)
![](/ReadmeImages/Returning-ticket.png)
![](/ReadmeImages/Returned-tickets.png)

## Additional information
The project uses role-based authentication so if you want to see all the features please log in as an employee or administrator with the following default credentials:  
__Admin username:__ superAdmin  
__Admin password:__ sYd&jDc@VU5ZVFn!

__Employee username:__ bestEmployee  
__Employee password:__ 4YDaZf@onq^7FxqH

