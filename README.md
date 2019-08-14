# JJBookStore
Fully-functional book store, ASP.NET MVC web app.

This is my first ASP.NET MVC web application. Users can search book and add to shopping cart to purchase it. Payment function module has not finished yet. In this book store, every user can be buyers and sellers. 

### Function detail
For user: Register, Sign in, Sign out, Show profile, Edit profile, Change password, Forget password, Manage Shopping cart

For book: Create, Edit, Show details, Add to shopping cart, Delete


********************************************************************************************************
## Things to do 
#### 13/8
- Hidden URL parameters while doing GET request(ie. user detail)
- Add authorization method instead of store in Session
- Bug of duplicate Email or Username can be registered

#### 14/8
- Find a Email Authentition method for user validation
  - Also forget password function related to email
- Solution for Session Cookie not timeout after close browser
- Book search box with seletive search method(passing two parameters)

#### 15/8




*******************************************************************************************************

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

### Installing

A step by step series of examples that tell you how to get a development env running

Say what the step will be



End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

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
