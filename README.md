Overview

This project is a web application that allows users to register, log in, and manage other users. After logging in, users can block, unblock, and delete other users. Note that users can block or delete themselves.
Features

    User Registration: New users can register with their email and password.
    User Login: Registered users can log in to access the application.
    User Management:
        Block Users: Logged-in users can block other users.
        Unblock Users: Logged-in users can unblock previously blocked users.
        Delete Users: Logged-in users can delete other users.
        Self-Management: Users are allowed to block or delete themselves.

Installation

    Clone the repository:

    bash

git clone https://github.com/yourusername/your-repo-name.git
cd your-repo-name

Setup the environment:

Make sure you have .NET Core SDK installed.

Configure the application:

Update the appsettings.json file with your database and email configurations.

Restore dependencies:

bash

dotnet restore

Run the application:

bash

    dotnet run

Usage

    Register:

    Open the application in your browser and navigate to the registration page. Provide the required information to create a new account.

    Login:

    After registering, log in using your credentials.

    Manage Users:

    Once logged in, you can:
        Block a user: Select a user from the list and choose the block option.
        Unblock a user: Select a previously blocked user and choose the unblock option.
        Delete a user: Select a user and choose the delete option.
        Self-Management: You can block or delete your own account.

Contributing

We welcome contributions! Please read our Contributing Guidelines before submitting a pull request.
License

This project is licensed under the MIT License. See the LICENSE file for details.
Contact

For any inquiries or issues, please contact your-email@example.com.

Feel free to customize this template to fit your project's specific needs and details.
