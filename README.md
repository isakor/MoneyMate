
# 💰 MoneyMate

**MoneyMate** is a personal finance web application developed for the DAVE3625 – Introduction to Artificial Intelligence course.

The application helps users manage their income, expenses, and monthly budgets through a simple and user-friendly interface.

## ✨ Features

- **Transaction Management** – Add, edit, delete, and filter income and expenses.
- **Budget Management** – Create, edit, and delete monthly budgets.
- **Financial Dashboard** – Track income, expenses, balance, and remaining budget.
- **Charts** – Visualize expenses by category and compare monthly income and expenses.
- **Filtering** – View financial data by month and year.
- **Input Validation** – Prevent invalid transaction and budget entries.

## 🛠️ Technologies

- C# and ASP.NET Core MVC
- Entity Framework Core
- SQLite
- HTML and Bootstrap
- JavaScript and Chart.js

## 🚀 Getting Started

**Requirements:** .NET 10 SDK.

1. Clone the repository:

   ```bash
   git clone https://github.com/isakor/MoneyMate.git
   ```

2. Open the project folder:

   ```bash
   cd MoneyMate
   ```

3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Start the application:

   ```bash
   dotnet run
   ```

5. Open the local URL shown in the terminal.

### Database Setup

MoneyMate uses SQLite for data storage.

The database and its tables are automatically created using Entity Framework Core migrations when the application starts.

No manual database setup is required.

The database starts empty, allowing users to add their own transactions and budgets.

## 🧪 Testing

The application has been manually tested to verify:

- Transaction creation, editing, deletion, and filtering.
- Monthly budget management.
- Dashboard calculations and financial charts.
- Month and year filtering.
- Input validation.

All 14 functional tests passed after debugging.

## 🔮 Future Improvements

Possible improvements include user authentication, individual user accounts, and exporting financial reports.

## 🎥 Demo

[▶ Watch the MoneyMate Demo on YouTube](https://www.youtube.com/watch?v=Q9am8s1yUKE)
