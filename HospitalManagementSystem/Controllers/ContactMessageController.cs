using HospitalManagementSystem.Dto.ContactMessage;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessageController : Controller
    {
        private readonly IContactMessageService _service;
        public ContactMessageController(IContactMessageService _service)
        {
            this._service = _service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _service.GetAll();
            if (messages == null) return NoContent();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await _service.GetMessage(id);
            if (message == null) return NoContent();
            return Ok(message);
        }

        [HttpGet("{id}/patients")]
        public async Task<IActionResult> GetMessagesOfPatient(int id)
        {
            var messages = await _service.GetMessagesOfPatient(id);
            if (messages == null) return NoContent();
            return Ok(messages);
        }


        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto messageDto)
        {
            var message = await _service.CreateMessage(messageDto);
            if (message == null) return NoContent();
            return Ok(message);
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var message = await _service.DeleteMessage(messageId);
            if (message == null) return NoContent();
            return Ok(message);
        }

        [HttpPut("{messageId}")]
        public async Task<IActionResult> UpdateMessage(int messageId, [FromBody] UpdateMessageDto messageDto)
        {
            var message = await _service.UpdateMessage(messageId, messageDto);
            if (message == null) return NoContent();
            return Ok(message);
        }
    }
}
