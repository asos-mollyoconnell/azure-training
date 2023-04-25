namespace CanonicalCustomerData.Models
{
    public class CanonicalCustomer
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int MobileNumber { get; set; }
        public string? Country { get; set; }
    }
}