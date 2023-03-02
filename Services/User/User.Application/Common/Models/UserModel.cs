namespace UserAccount.Application.Common.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? About { get; set; }
        public string? Image { get; set; }
    }
}
