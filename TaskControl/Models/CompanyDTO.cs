namespace TaskControl.Models
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string BusinessName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
