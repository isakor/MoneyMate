
using Microsoft.EntityFrameworkCore;
using MoneyMate.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

// Configure SQLite database
builder.Services.AddDbContext<MoneyMateDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("MoneyMateDbContext")
    )
);

// Register repositories
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();

var app = builder.Build();

// Automatically create and update the SQLite database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<MoneyMateDbContext>();

    context.Database.Migrate();
}

// Configure error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Configure default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
