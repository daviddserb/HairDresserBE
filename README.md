# HairDresser

Hair Dresser it's web application that allows customers to make appointments for their needs. To use it, you need to register an account.
There are three types of users:
  1. Admin:
- CRUD hair services.
- View all appointments and even the canceled ones.
- View all employees with their hair services and working intervals.
  2. Employee:
- Add the hair services that they are capable of doing.
- View all his appointments.
- Make their working intervals, which can be multiple in a working day.
  3. Customer:
- Make appointments, which means: selecting hair services, choosing an employee, picking a date, and choosing the interval time.
- View all his appointments, with the option to cancel only those whose start time is at least one day from the current day.

I used the following:
  - back-end: C#, ASP .NET Core Web API, Database: Microsoft SQL Server with ORM: Entity Framework Core.
  - front-end: Angular with Material UI.
