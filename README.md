
# 💰 MoneyMate

**MoneyMate** is a personal finance web application developed for the DAVE3625 – Introduction to Artificial Intelligence course.

The application helps users manage their income, expenses, and monthly budgets through a simple and user-friendly interface.

## ✨ Features

- **Transaction Management** – Add, edit, delete, and filter transactions.
- **Budget Management** – Create and manage monthly budgets.
- **Financial Dashboard** – Track income, expenses, and balance.
- **Charts** – Visualize spending by category and monthly finances.
- **Filtering** – Filter financial data by month and year.

## 🛠️ Technologies

- ASP.NET Core MVC / C#
- Entity Framework Core
- SQLite
- Bootstrap
- JavaScript / Chart.js


## 🚀 Getting Started

**Requirements:** .NET 10 SDK and EF Core CLI tools.

1. Clone the repository and open the MoneyMate project folder.

2. Restore the project dependencies:

   ```bash
   dotnet restore
   ```

3. Install the EF Core CLI tool if it is not already installed:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. Create the SQLite database using the existing migrations:

   ```bash
   dotnet ef database update
   ```

5. Start the application:

   ```bash
   dotnet run
   ```

6. Open the local URL shown in the terminal.
