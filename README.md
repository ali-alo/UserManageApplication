# User Management Web Application

This is a web application that allows users to register, authenticate, and manage user accounts. It provides features for blocking, unblocking, and deleting user accounts, as well as displaying user details in a tabular format. For quick access go to the deployed application: https://ali-alo-usermanageapp.azurewebsites.net/

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Usage](#usage)

## Features

- **Registration and Authentication**: Users can create accounts and log in securely.
- **User Management Panel**: Only authenticated users have access to the user management panel.
- **User Table**: Displays user details, including ID, name, email, last login time, registration time, and status (active/blocked).
- **Multiple Selection**: The table allows users to select multiple records using checkboxes.
- **Toolbar Actions**: A toolbar above the table provides actions for blocking, unblocking, and deleting users.

## Technologies

- ASP.NET Core 6.0
- Entity Framework Core 6.0
- Bootstrap 5
- SQL Server 2019

## Getting Started

To get started with this project, follow these steps:

1. Clone the repository to your local machine:

   ```shell
   git clone https://github.com/ali-alo/UserManageApplication.git
    ```

2. Navigate to the project folder:
    ```shell
    cd UserManageApplication
    ```

2. Install the required dependencies by running:
    ```shell
    dotnet restore
    ```

3. Apply the database migrations:
    ```shell
    dotnet ef database update
    ```

4. Build and run the application:
    ```shell
    dotnet run
    ```
5. Access the application in your web browser at https://localhost:7246/

## Usage

1. Register or log in as a user.

2. Access the user management panel to view the table of user accounts.

3. Use checkboxes in the leftmost column to select one or more user accounts.

4. Use the toolbar actions to block, unblock, or delete selected users.

5. Blocked or deleted users will be redirected to the login page on subsequent requests.

6. Users can use any non-empty password, including a single character.

7. Blocked users cannot log in, but deleted users can re-register.

8. Users can block, or delete their own accounts and will be redirected to the login page.