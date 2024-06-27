<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Project Name</title>
</head>
<body>
  <h1>Project Name</h1>
  <p><a href="LICENSE"><img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="License"></a></p>

  <h2>Overview</h2>
  <p>This project is a web application that allows users to register, log in, and manage other users. After logging in, users can block, unblock, and delete other users. Note that users can block or delete themselves.</p>

  <h2>Features</h2>
  <ul>
    <li><strong>User Registration:</strong> New users can register with their email and password.</li>
    <li><strong>User Login:</strong> Registered users can log in to access the application.</li>
    <li><strong>User Management:</strong>
      <ul>
        <li><strong>Block Users:</strong> Logged-in users can block other users.</li>
        <li><strong>Unblock Users:</strong> Logged-in users can unblock previously blocked users.</li>
        <li><strong>Delete Users:</strong> Logged-in users can delete other users.</li>
        <li><strong>Self-Management:</strong> Users are allowed to block or delete themselves.</li>
      </ul>
    </li>
  </ul>

  <h2>Installation</h2>
  <ol>
    <li><strong>Clone the repository:</strong>
      <pre><code>git clone https://github.com/yourusername/your-repo-name.git
cd your-repo-name
      </code></pre>
    </li>
    <li><strong>Setup the environment:</strong>
      <p>Make sure you have <a href="https://dotnet.microsoft.com/download">.NET Core SDK</a> installed.</p>
    </li>
    <li><strong>Configure the application:</strong>
      <p>Update the <code>appsettings.json</code> file with your database and email configurations.</p>
      <pre><code>{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Default": "YourDatabaseConnectionString"
  },
  "JWT": {
    "Key": "YourJWTKey",
    "Audience": "YourJWTAudience",
    "Issuer": "YourJWTIssuer",
    "AccessTokenExpireMinutes": 20,
    "RefreshTokenValidityHours": 5640
  },
  "Email": {
    "Host": "smtp.your-email-provider.com",
    "EmailAddress": "your-email@example.com",
    "Password": "your-email-password"
  },
  "BaseUrl": "http://localhost:7053"
}
      </code></pre>
    </li>
    <li><strong>Restore dependencies:</strong>
      <pre><code>dotnet restore</code></pre>
    </li>
    <li><strong>Run the application:</strong>
      <pre><code>dotnet run</code></pre>
    </li>
  </ol>

  <h2>Usage</h2>
  <ol>
    <li><strong>Register:</strong>
      <p>Open the application in your browser and navigate to the registration page. Provide the required information to create a new account.</p>
    </li>
    <li><strong>Login:</strong>
      <p>After registering, log in using your credentials.</p>
    </li>
    <li><strong>Manage Users:</strong>
      <p>Once logged in, you can:</p>
      <ul>
        <li><strong>Block a user:</strong> Select a user from the list and choose the block option.</li>
        <li><strong>Unblock a user:</strong> Select a previously blocked user and choose the unblock option.</li>
        <li><strong>Delete a user:</strong> Select a user and choose the delete option.</li>
        <li><strong>Self-Management:</strong> You can block or delete your own account.</li>
      </ul>
    </li>
  </ol>

  <h2>Contributing</h2>
  <p>We welcome contributions! Please read our <a href="CONTRIBUTING.md">Contributing Guidelines</a> before submitting a pull request.</p>

  <h2>License</h2>
  <p>This project is licensed under the MIT License. See the <a href="LICENSE">LICENSE</a> file for details.</p>

  <h2>Contact</h2>
  <p>For any inquiries or issues, please contact <a href="mailto:your-email@example.com">your-email@example.com</a>.</p>
</body>
</html>
