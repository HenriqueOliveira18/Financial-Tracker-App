using System.ComponentModel;
using Tracker.Domain.Enums;

namespace Tracker.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType? TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
        public string? Description { get; set; }

        // Foreign key
        public int AccountId { get; set; }
        // Navigation property
        public virtual Account Account { get; set; }

    }
}
