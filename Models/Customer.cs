namespace YourNamespace.Models
{
    public class Customer
    {
        public int Id { get; set; } // Primary Key
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}