namespace lab4_quick.Domain.Entities
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;


        public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
    }
}
