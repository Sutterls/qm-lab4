using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IMeeting;
using lab4_quick.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab4_quick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly ILogger<MeetingController> _logger;

        public MeetingController(IMeetingService meetingService, ILogger<MeetingController> logger)
        {
            _meetingService = meetingService ?? throw new ArgumentNullException(nameof(meetingService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetingById(Guid id)
        {
            try
            {
                var meeting = await _meetingService.GetMeetingByIdAsync(id);
                if (meeting == null)
                {
                    _logger.LogWarning("Встреча с id {Id} не найдена", id);
                    return NotFound("Встреча не найдена");
                }

                return Ok(meeting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении встречи с id {Id}", id);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllMeetingsByUserId(Guid userId)
        {
            try
            {
                var meetings = await _meetingService.GetAllMeetingsByUserIdAsync(userId);
                return Ok(meetings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении встреч пользователя с id {UserId}", userId);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeeting([FromBody] Meeting meeting)
        {
            try
            {
                if (meeting == null)
                {
                    return BadRequest("Данные встречи не могут быть пустыми");
                }

                await _meetingService.AddMeetingAsync(meeting.UserId, meeting.Title, meeting.Description, meeting.StartTime, meeting.EndTime);
                return CreatedAtAction(nameof(GetMeetingById), new { id = meeting.Id }, meeting);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "Ошибка при создании встречи");
                return Conflict("Ошибка при создании встречи");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании встречи");
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeeting(Guid id, [FromBody] Meeting meeting)
        {
            try
            {
                if (meeting == null)
                {
                    return BadRequest("Данные встречи не могут быть пустыми");
                }

                await _meetingService.UpdateMeetingAsync(id, meeting.Title, meeting.Description, meeting.StartTime, meeting.EndTime, meeting.Status);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Встреча с id {Id} не найдена", id);
                return NotFound("Встреча не найдена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении встречи с id {Id}", id);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeeting(Guid id)
        {
            try
            {
                await _meetingService.DeleteMeetingAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Встреча с id {Id} не найдена", id);
                return NotFound("Встреча не найдена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении встречи с id {Id}", id);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpGet("upcoming/user/{userId}")]
        public async Task<IActionResult> GetUpcomingMeetingsByUserId(Guid userId)
        {
            try
            {
                var upcomingMeetings = await _meetingService.GetUpcomingMeetingsByUserIdAsync(userId);
                return Ok(upcomingMeetings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении предстоящих встреч пользователя с id {UserId}", userId);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }
    }
}
