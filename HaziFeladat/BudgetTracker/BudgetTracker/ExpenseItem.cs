using System;

namespace BudgetTracker
{
    public record ExpenseItem(string Name, int Amount, string Category, DateTime Date);
}