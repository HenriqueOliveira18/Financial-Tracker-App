namespace Tracker.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        // Navigation property for related Transaction rows
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
