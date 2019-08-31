# JJBookStore
Fully-functional book store, ASP.NET MVC web app.

This is my first ASP.NET MVC web application. Users can search book and add to shopping cart to purchase it. Payment function module has not finished yet. In this book store, every user can be buyers and sellers. 

### Function detail
For user: Register, Sign in, Sign out, Show profile, Edit profile, Change password, Forget password, Manage Shopping cart, Purchase Book

For book: Create, Edit, Show details, Add to shopping cart, Delete


********************************************************************************************************************
## Things to do 
#### 13/8 Tue
- Hidden URL parameters while doing GET request(ie. user detail)(DONE)
- Add authorization method instead of store in Session(DONE, by using FormsAuthenticationTicket Class to store userdata into cookie)
- Bug of duplicate Email or Username can be registered(DONE)

#### 14/8 Wed
- Find a Email Authentition method for user validation(DONE)
  - Also forget password function related to email
- Solution for Session Cookie not timeout after close browser(DONE, by using FormsAuthenticationTicket to avoid using session)
- Book search box with seletive search method(passing two parameters)(DONE)

#### 15/8 Thu
- ImgURL for book display, and set default img if does not upload one
- Pop up window for delete(DONE)
- Dynamically calculate price without refreshing page(AJAX) (DONE by JS)
- Shopping Cart page management improve, price should be unit price * amount(AKA quantity) (DONE)

#### 16/8 Fri
- Go back link to previous page(DONE)
- Format model and view model constructor and convert functions,to avoid them occurs in controller(DONE)
#### 17/8 Sat
- Multiple submission from same form into different controllers.(DONE)
- Confirmation dialog box before submittion a POST form. (DONE)
#### 18/8 Sun
- Purchase button submitting hold until new poped-up confirmation page confirmed. Or confirmation dialog before submitting, while pop-up a new payment page.(DONE, but not available for Goole Chrome, becase of the dialog block policy, details: https://www.chromestatus.com/feature/5140698722467840 )
- POST request redirect to another POST method, or to GET method with object transfer.(Abandoned)
#### 19/8 Mon
- Sellers inspect sales-records for each selling book.(DONE)
- Inner website message function between users.(Abandoned)
- Check if at least one checkbox been checked before submitting at shopping cart(DONE JQuery)

#### 20/8 Tue
- User Telerik UI to build an Administration Database Management site.(DONE)
- Add ForeignKey Column into Admin Site, and optimize the layout.(DONE)
- Telerik Datepicker validation format always wrong.
#### 21/8 Wed
- Find an easiest way to use Dreamwaver UI template to improve UI.
- Users shouldn't be able to purchase a book which seller is themselves.

### Deploy on server
#### Server Environment
```
Instance: AWS EC2 Instance
AMI: Microsoft Windows Server 2019 Base
Database: MS SQL Express 2016
IIS Management with .NET 4.7
```

#### Common bugs fix after deploy
- Set Sql Server new login to app-pool user in SSMS Security table
- Generate new Elastic IP address for aws ec2 instance
- Map domain name to Elastic ip address at route 53 by adding a new hosted zones
- Add new security group and set inbound rule and allow to access to specific port(e.g. port 80)
- Modify Data-souce attribute in connection string at web.config
- Replace all absolute path and url with relative ones
- Set security permission of folder "Upload" as everyone can write and read
- BundleConfig abandoned, import js and css file in traditional way 
- Do not have a SMTP Email account, so ban auto email function temporarily


***************************************************************************************************************

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them
```
Give examples
```
##### Development Environment
```
Windows 10 Home 64bit
Visual Studio Professional 2017
Microsoft SQL Server 2017
Google Chrome Version 76.0
```

##### Environment Setup

Create new ASP.NET Web Application(.NET Framework) in Visual Studio, Choose MVC Template with No Authentition.
Install References through Nuget Package
```
Install-Package EntityFramework
```
Use EntityFramework Code First method to create Database tables through exsiting Model files
Add migrations to Database
```
Enable-Migrations
Add-Migration 0001
Update-Database -Verbose
```
Install Dynamic Linq package
```
Install-Package System.Linq.Dynamic
```
Install PagedList for MVC via NuGet
```
Install-Package PagedList.mvc
```
### Installing

A step by step series of examples that tell you how to get a development env running

Say what the step will be


End with an example of getting some data out of the system or using it for a little demo

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Billie Thompson** - *Initial work* - [PurpleBooth](https://github.com/PurpleBooth)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
