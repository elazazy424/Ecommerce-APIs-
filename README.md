
## E-Commerce Website Built Using .NET

### Overview

Our e-commerce website is a robust, scalable, and secure online shopping platform developed using the .NET framework. It provides a seamless user experience and follows best practices in software design and architecture.

### Key Features

1. **Product Management**: Comprehensive features for managing product listings, categories, and inventories.
2. **Order Management**: Efficient order processing, tracking, and history management.
3. **Secure Authentication**: User authentication and authorization using ASP.NET Identity.
4. **Signup and Login**: Secure user registration and login features using ASP.NET Identity framework.
5. **Payment Processing**: Secure and reliable payment processing using Stripe with webhook integration.
6. **Database Management**: Robust data storage and retrieval using SQL Server.

### Technical Architecture

1. **.NET Framework**: Utilizes the power and flexibility of the .NET framework for a high-performance web application.
2. **SOLID Principles**: Adheres to SOLID principles to ensure the system is modular, maintainable, and scalable.
   - **Single Responsibility Principle**: Each class and method has a single responsibility.
   - **Open/Closed Principle**: The system is open for extension but closed for modification.
   - **Liskov Substitution Principle**: Derived classes can be substituted for their base classes without altering the correctness of the program.
   - **Interface Segregation Principle**: The system avoids large interfaces, opting for smaller, more specific ones.
   - **Dependency Inversion Principle**: High-level modules do not depend on low-level modules but on abstractions.

3. **Data Access Layer (DAL)**: Manages database interactions using Entity Framework, providing an abstraction over the SQL database.
4. **Business Logic Layer (BLL)**: Encapsulates the business rules and logic, ensuring a clean separation from data access and presentation layers.

### Secure Authentication

- **ASP.NET Identity Framework**: Manages user registration, login, and authentication processes. Ensures secure password hashing and token-based authentication.
- **Role-Based Access Control**: Supports different user roles (e.g., admin, customer) to provide appropriate access controls and permissions.

### Signup and Login

- **User Registration**: Allows new users to sign up with secure password storage and validation.
- **Login**: Enables existing users to log in securely using their credentials. Supports features like password recovery and account lockout after multiple failed attempts.
- **Email Confirmation**: Ensures user email verification through email confirmation links during registration.
- **Two-Factor Authentication**: Optional two-factor authentication for added security.

### Payment Processing

- **Stripe Integration**: Secure payment processing using Stripe. Integrates Stripe’s API for handling transactions, refunds, and payment disputes.
- **Webhooks**: Implements Stripe webhooks to handle asynchronous events such as payment confirmations and refunds.

### Error Management

- **API Response Error Class**: Centralized error handling using a custom API response error class. Ensures consistent and informative error responses across the application.
- **Logging and Monitoring**: Utilizes logging frameworks (such as Serilog) to monitor application performance and errors in real-time.

### Project Structure

1. **Main Project**: The core web application, developed using ASP.NET Core MVC, handles routing, views, and controllers.
2. **DAL (Data Access Layer)**:
   - **Repositories**: Classes that handle CRUD operations and interactions with the SQL database using Entity Framework.
   - **Migrations**: Manages database schema changes.
3. **BLL (Business Logic Layer)**:
   - **Services**: Business logic services that process data and apply business rules.
   - **DTOs (Data Transfer Objects)**: Encapsulates data for transfer between layers, ensuring a clean separation of concerns.
4. **Web API**: Exposes RESTful endpoints for front-end interactions, ensuring a decoupled and scalable architecture.
5. **Unit Tests**: Comprehensive unit tests to ensure code reliability and prevent regression.

###Swagger Screenshots
![image](https://github.com/elazazy424/Ecommerce-APIs-/assets/73352569/2122654f-e0e1-405d-9bdd-58e656f46589)
![image](https://github.com/elazazy424/Ecommerce-APIs-/assets/73352569/9d2f2239-da09-4690-a340-d25599eaf35b)
![image](https://github.com/elazazy424/Ecommerce-APIs-/assets/73352569/e2092ce5-069c-4dfd-98d0-eb46a34658cc)

### Conclusion

This e-commerce website, built using the .NET framework, follows best practices in software development to deliver a robust and scalable platform. With a solid architecture, secure payment processing, efficient error management, and a clear separation of concerns through DAL and BLL, it is designed to provide a seamless and secure shopping experience for users. The integration of ASP.NET Identity for secure signup and login further enhances the platform's security and user management capabilities.

---
