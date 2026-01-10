using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetTracker
{
    public class ExpenseManager
    {
        private List<ExpenseItem> _items;
        private readonly FileService _fileService;

        public ExpenseManager()
        {
            _fileService = new FileService();
            // Adatok betoltese
            _items = _fileService.LoadData();
        }

        public void AddExpense(string name, int amount, string category)
        {
            // 0-nal kisebb osszeg ervenytelen
            if (amount <= 0)
            {
                throw new ArgumentException("Az osszegnek pozitivnak kell lennie!");
            }

            var newItem = new ExpenseItem(name, amount, category, DateTime.Now);
            _items.Add(newItem);

            // Frissitjuk fajlban is
            _fileService.SaveData(_items);
        }

        public List<ExpenseItem> GetAllExpenses()
        {
            return _items;
        }

        public List<ExpenseItem> SearchExpenses(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return new List<ExpenseItem>();

            return _items.Where(x => x.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}