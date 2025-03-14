# Student Management System

## Overview

The Student Management System is a comprehensive application built using .NET 8.0. It provides functionalities to manage students and their enrollments, including creating, updating, and deleting student records, as well as handling student enrollments.

## Projects

The solution consists of the following projects:

1. **StudentManagement.API**: The main API project that exposes endpoints for managing students and enrollments.
2. **StudentManagement.Domain**: Contains the domain logic, including commands, queries, and handlers.
3. **StudentManagement.Models**: Contains the data models used across the solution.
4. **StudentManagement.Repository**: The repository layer for data access.
5. **StudentManagement.UnitTests**: Contains unit tests for the solution.

## Prerequisites

- .NET 8.0 SDK
- PostgreSQL

## Getting Started

### Configuration

1. **Database Configuration**: Update the connection string in `appsettings.json` located in the `StudentManagement.API` project.
2. **API Versioning Configuration**: Ensure the supported versions and default version are correctly set in `appsettings.json`.
3. **Run the Application**: Start the application by running the `StudentManagement.API` project.
4. **Swagger UI**: Open the browser and navigate to `https://localhost:5001/swagger` to access the Swagger UI for API documentation and testing.

### Running the Tests

1. **Run Unit Tests**: Execute the unit tests in the `StudentManagement.UnitTests` project.


## API Endpoints

### Students

- **Get All Students (v1)**: `GET /api/Students`
- **Get All Students (v2)**: `GET /api/Students`
- **Get Student by ID**: `GET /api/Students/{studentId}`
- **Create Student**: `POST /api/Students`
- **Update Student**: `PUT /api/Students/{studentId}`
- **Activate Student**: `PATCH /api/Students/{studentId}/activate`
- **Remove Student**: `DELETE /api/Students/{studentId}`

### Enrollments

- **Get All Enrollments**: `GET /api/Enrollments`
- **Get Enrollment by ID**: `GET /api/Enrollments/{studentId}`

## Technologies Used

- .NET 8.0
- Entity Framework Core
- MediatR
- FluentValidation
- PostgreSQL
- Swagger
- xUnit
- Moq

## License

This project is licensed under the MIT License.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request for review.

## Additional Information

For Additional information, please refer [here](https://emishealthgroup.atlassian.net/wiki/spaces/DIA/pages/7690780737/Student+Management+-+POC).





    



    



    

