using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;



namespace lab4_quick.Domain.Entities
{
    public partial class Meeting
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual User User { get; set; } = null!;

        public enum MeetingStatus
        {
            Active,
            Canceled,
            Completed
        }
        public MeetingStatus Status { get; set; }

        public void CancelMeeting()
        {
            if (Status == MeetingStatus.Active)
            {
                Status = MeetingStatus.Canceled;
            }
        }

        public void CompleteMeeting()
        {
            if (Status == MeetingStatus.Active)
            {
                Status = MeetingStatus.Completed;
            }
        }
        public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}


