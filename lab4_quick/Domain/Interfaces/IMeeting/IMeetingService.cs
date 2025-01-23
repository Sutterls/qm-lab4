using lab4_quick.Domain.Entities;

namespace lab4_quick.Domain.Interfaces.IMeeting
{

    public interface IMeetingService
    {
        Task<Meeting?> GetMeetingByIdAsync(Guid id);
        Task<IEnumerable<Meeting>> GetAllMeetingsByUserIdAsync(Guid userId);
        Task AddMeetingAsync(Guid userId, string title, string? description, DateTime startTime, DateTime endTime);
        Task UpdateMeetingAsync(Guid id, string title, string? description, DateTime startTime, DateTime endTime, Meeting.MeetingStatus status);
        Task DeleteMeetingAsync(Guid id);
        Task<IEnumerable<Meeting>> GetUpcomingMeetingsByUserIdAsync(Guid userId);
    }
}