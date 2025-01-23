namespace lab4_quick.Domain.Entities
{
    public partial class Invitation
    {
        public Guid Id { get; set; }

        public Guid MeetingId { get; set; }

        public string InviteeEmail { get; set; } = null!;

        public enum InvitationStatus
        {
            Pending,
            Accepted,
            Declined
        }
        public InvitationStatus Status { get; set; }

        public void Accept()
        {
            if (Status == InvitationStatus.Pending)
            {
                Status = InvitationStatus.Accepted;
            }
        }

        public void Decline()
        {
            if (Status == InvitationStatus.Pending)
            {
                Status = InvitationStatus.Declined;
            }
        }
        public virtual Meeting Meeting { get; set; } = null!;
    }
}
