namespace Senswise.Application
{
    public class CreateCustomerCommand
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Address { get; set; }
    }
}
