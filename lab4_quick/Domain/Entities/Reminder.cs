namespace lab4_quick.Domain.Entities
{
    public partial class Reminder
    {
        public Guid Id { get; set; }

        public Guid MeetingId { get; set; }

        public DateTime ReminderTime { get; set; }

        public virtual Meeting Meeting { get; set; } = null!; 
    }
}
