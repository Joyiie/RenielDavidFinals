namespace RenielDavid.webdev.Infrastructure.Domain.Models
{
    public class UserLogin
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Type { get; set; } //Google, Facebook, General
        public string? Key { get; set; }
        public string? Value { get; set; }
    }

}