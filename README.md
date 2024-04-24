# Special-Topics-in-Software-Engineering

This is an Expenses application created by me, grandvx and Antonisvar for a university project in the course of Special Topics in Software Engineering.
   
## Description

This repository contains the backend code for the Expenses application, a full-stack React and .NET application designed to help users keep track of their expenses.
We have designed and developed a business application of CRUD type, three levels (front-end, business logic, database) on a small scale. By "small scale" we mean that it is not necessary to deal with all possible functions of such an application but to select 3-4 of those that are important. The aim of the task is not only the software to be produced but also the development methodology.

## Overview

The Expenses backend is built using .NET Core, a cross-platform framework for building scalable and high-performance applications. It provides the necessary APIs and endpoints for the frontend to interact with the database and manage expenses data.


## A. Development Methodology

Τhe design and development should follow the principles of agile development i.e. at a minimum, the requirements will be in user stories, there should be a product backlog and a board for each sprint.
The tool that we will use for project management (in their free version) is Trello.

## B. System Architecture

As mentioned above, our application will have three layers (front-end, business logic, database).
The constraints we follow are
(a) The front-end will communicate with the business logic using RESTful web services
(b) The business logic is based on an object-oriented language programming language
(c) The database must be relational and
(d) The business logic must communicate with the database via ORM.


## Technologies Used

* ASP.NET Core Web API
* Entity Framework Core (for database access)
* MS SQL Server

## Setup Instructions

1. Clone this repository to your local machine.
2. Navigate to project directory.
3. Open the solution file 'Expenses.sln' in Visual Studio or your preferred IDE.
4. Build the solution to restore dependencies and compile the project.
5. Run the application. (Using the play button at the top, but in http not in https mode).
