using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IInvitation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab4_quick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService _invitationService;
        private readonly ILogger<InvitationController> _logger;

        public InvitationController(IInvitationService invitationService, ILogger<InvitationController> logger)
        {
            _invitationService = invitationService ?? throw new ArgumentNullException(nameof(invitationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvitationById(Guid id)
        {
            try
            {
                var invitation = await _invitationService.GetInvitationByIdAsync(id);
                if (invitation == null)
                {
                    _logger.LogWarning("Приглашение с id {Id} не найдено", id);
                    return NotFound("Приглашение не найдено");
                }

                return Ok(invitation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении приглашения с id {Id}", id);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvitation([FromBody] Invitation invitation)
        {
            try
            {
                if (invitation == null)
                {
                    return BadRequest("Данные приглашения не могут быть пустыми");
                }

                await _invitationService.CreateInvitationAsync(invitation.MeetingId, invitation.InviteeEmail);
                return CreatedAtAction(nameof(GetInvitationById), new { id = invitation.Id }, invitation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании приглашения");
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvitation(Guid id, [FromBody] Invitation invitation)
        {
            try
            {
                if (invitation == null)
                {
                    return BadRequest("Данные приглашения не могут быть пустыми");
                }

                await _invitationService.UpdateInvitationAsync(id, invitation.Status);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Приглашение с id {Id} не найдено", id);
                return NotFound("Приглашение не найдено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении приглашения с id {Id}", id);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvitation(Guid id)
        {
            try
            {
                await _invitationService.DeleteInvitationAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Приглашение с id {Id} не найдено", id);
                return NotFound("Приглашение не найдено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении приглашения с id {Id}", id);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }

        [HttpGet("meeting/{meetingId}")]
        public async Task<IActionResult> GetInvitationsByMeetingId(Guid meetingId)
        {
            try
            {
                var invitations = await _invitationService.GetInvitationsByMeetingIdAsync(meetingId);
                return Ok(invitations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении приглашений для встречи с id {MeetingId}", meetingId);
                return StatusCode(500, "Произошла неизвестная ошибка");
            }
        }
    }
}

