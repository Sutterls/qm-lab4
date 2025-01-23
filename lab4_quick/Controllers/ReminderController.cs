using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IReminder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab4_quick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;
        private readonly ILogger<ReminderController> _logger;

        public ReminderController(IReminderService reminderService, ILogger<ReminderController> logger)
        {
            _reminderService = reminderService ?? throw new ArgumentNullException(nameof(reminderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReminderById(Guid id)
        {
            try
            {
                var reminder = await _reminderService.GetReminderByIdAsync(id);
                if (reminder == null)
                {
                    _logger.LogWarning("Напоминание с id {Id} не найдено", id);
                    return NotFound("Напоминание не найдено");
                }

                return Ok(reminder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении напоминания с id {Id}", id);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpGet("meeting/{meetingId}")]
        public async Task<IActionResult> GetAllRemindersByMeetingId(Guid meetingId)
        {
            try
            {
                var reminders = await _reminderService.GetAllRemindersByMeetingIdAsync(meetingId);
                return Ok(reminders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении напоминаний для встречи с id {MeetingId}", meetingId);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReminder([FromBody] Reminder reminder)
        {
            try
            {
                if (reminder == null)
                {
                    return BadRequest("Данные напоминания не могут быть пустыми");
                }

                await _reminderService.AddReminderAsync(reminder.MeetingId, reminder.ReminderTime);
                return CreatedAtAction(nameof(GetReminderById), new { id = reminder.Id }, reminder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении напоминания");
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReminder(Guid id, [FromBody] Reminder reminder)
        {
            try
            {
                if (reminder == null)
                {
                    return BadRequest("Данные напоминания не могут быть пустыми");
                }

                await _reminderService.UpdateReminderAsync(id, reminder.ReminderTime);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Напоминание с id {Id} не найдено", id);
                return NotFound("Напоминание не найдено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении напоминания с id {Id}", id);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(Guid id)
        {
            try
            {
                await _reminderService.DeleteReminderAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Напоминание с id {Id} не найдено", id);
                return NotFound("Напоминание не найдено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении напоминания с id {Id}", id);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }
    }
}
