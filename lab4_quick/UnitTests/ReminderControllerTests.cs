using lab4_quick.Controllers;
using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IReminder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab4_quick.UnitTests
{
    [TestClass]
    public class ReminderControllerTests
    {
        private Mock<IReminderService> _serviceMock = null!;
        private Mock<ILogger<ReminderController>> _loggerMock = null!;
        private ReminderController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IReminderService>();
            _loggerMock = new Mock<ILogger<ReminderController>>();
            _controller = new ReminderController(_serviceMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetReminderById_Success()
        {
            var reminderId = Guid.NewGuid();
            var reminder = new Reminder
            {
                Id = reminderId,
                MeetingId = Guid.NewGuid(),
                ReminderTime = DateTime.Now
            };

            _serviceMock.Setup(x => x.GetReminderByIdAsync(reminderId)).ReturnsAsync(reminder);

            var result = await _controller.GetReminderById(reminderId);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(reminder, okResult.Value);
        }

        [TestMethod]
        public async Task GetReminderById_NotFound()
        {
            var reminderId = Guid.NewGuid();
            _serviceMock.Setup(x => x.GetReminderByIdAsync(reminderId)).ReturnsAsync(value: null as Reminder);

            var result = await _controller.GetReminderById(reminderId);

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("Напоминание не найдено", notFoundResult?.Value);
        }

        [TestMethod]
        public async Task GetReminderById_InternalServerError()
        { 
            var reminderId = Guid.NewGuid();
            _serviceMock.Setup(x => x.GetReminderByIdAsync(reminderId))
                        .ThrowsAsync(new Exception("Internal server error"));

            var result = await _controller.GetReminderById(reminderId);

            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult?.StatusCode);
            Assert.AreEqual("Произошла неизвестная ошибка", objectResult?.Value);
        }

        [TestMethod]
        public async Task GetAllRemindersByMeetingId_Success()
        {
            var meetingId = Guid.NewGuid();
            var reminders = new List<Reminder>
            {
                new() { Id = Guid.NewGuid(), MeetingId = meetingId, ReminderTime = DateTime.Now },
                new() { Id = Guid.NewGuid(), MeetingId = meetingId, ReminderTime = DateTime.Now.AddMinutes(10) }
            };

            _serviceMock.Setup(x => x.GetAllRemindersByMeetingIdAsync(meetingId)).ReturnsAsync(reminders);

            var result = await _controller.GetAllRemindersByMeetingId(meetingId);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(reminders, okResult.Value);
        }

    }
}
