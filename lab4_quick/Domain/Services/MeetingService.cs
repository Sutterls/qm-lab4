using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IMeeting;

namespace lab4_quick.Domain.Services
{

    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingService(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<Meeting?> GetMeetingByIdAsync(Guid id)
        {
            return await _meetingRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Meeting>> GetAllMeetingsByUserIdAsync(Guid userId)
        {
            return await _meetingRepository.GetAllByUserIdAsync(userId);
        }

        public async Task AddMeetingAsync(Guid userId, string title, string? description, DateTime startTime, DateTime endTime)
        {
            var meeting = new Meeting
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = title,
                Description = description,
                StartTime = startTime,
                EndTime = endTime,
                Status = Meeting.MeetingStatus.Active
            };

            await _meetingRepository.AddAsync(meeting);
        }

        public async Task UpdateMeetingAsync(Guid id, string title, string? description, DateTime startTime, DateTime endTime, Meeting.MeetingStatus status)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
            if (meeting == null)
            {
                throw new Exception("Встреча не найдена");
            }

            meeting.Title = title;
            meeting.Description = description;
            meeting.StartTime = startTime;
            meeting.EndTime = endTime;
            meeting.Status = status;

            await _meetingRepository.UpdateAsync(meeting);
        }

        public async Task DeleteMeetingAsync(Guid id)
        {
            await _meetingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Meeting>> GetUpcomingMeetingsByUserIdAsync(Guid userId)
        {
            return await _meetingRepository.GetUpcomingByUserIdAsync(userId);
        }
    }
}
