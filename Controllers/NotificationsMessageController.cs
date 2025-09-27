using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationsApi.Data;
using NotificationsApi.Dtos;
using NotificationsApi.Models;

namespace NotificationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsMessageController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetAll()
        {
            var notifications = await _context.NotificationMessages.ToListAsync();

            var result = notifications
        .OrderBy(n => n.NotificationMessageID) 
        .Select(n => new NotificationDto
        {
            MessageId = "N" + n.NotificationMessageID.ToString("D3"),
            NotificationMessageID = n.NotificationMessageID,
            NotificationChannel = n.NotificationChannel,
            NotificationHeading = n.NotificationHeading,
            NotificationBody = n.NotificationBody,
            NotificationFooter = n.NotificationFooter,
            NotificationSubject = n.NotificationSubject,
            RepeatEvery = n.RepeatEvery,
            NoOfTimesToRepeat = n.NoOfTimesToRepeat,
            CreatedBy = n.CreatedBy,
            CreatedDate = n.CreatedDate,
            UpdatedBy = n.UpdatedBy,
            UpdatedDate = n.UpdatedDate,
            RepeatNotification = n.RepeatNotification,
            UseDocumentTemplate = n.UseDocumentTemplate,
            DocumentTemplateID = n.DocumentTemplateID,
        })
        .ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           
            var n = await _context.NotificationMessages.FindAsync(id);
            if (n == null) return NotFound();

            var dto = new NotificationDto
            {
                MessageId= "N" + n.NotificationMessageID.ToString("D3"),
                NotificationMessageID = n.NotificationMessageID,
                NotificationChannel = n.NotificationChannel,
                NotificationHeading = n.NotificationHeading,
                NotificationBody = n.NotificationBody,
                NotificationFooter = n.NotificationFooter,
                NotificationSubject = n.NotificationSubject,
                RepeatEvery = n.RepeatEvery,
                NoOfTimesToRepeat = n.NoOfTimesToRepeat,
                CreatedBy = n.CreatedBy,
                CreatedDate = n.CreatedDate,
                UpdatedBy = n.UpdatedBy,
                UpdatedDate = n.UpdatedDate,
                RepeatNotification = n.RepeatNotification,
                UseDocumentTemplate = n.UseDocumentTemplate,
                DocumentTemplateID = n.DocumentTemplateID
            };

            return Ok(dto);

        }

        [HttpPost]
        public async Task<ActionResult<NotificationMessage>> Add(AddNotificationDto dto)
        {
            var notification = new NotificationMessage
            {
                NotificationChannel = dto.NotificationChannel,
                NotificationHeading = dto.NotificationHeading,
                NotificationBody = dto.NotificationBody,
                NotificationFooter = dto.NotificationFooter,
                NotificationSubject = dto.NotificationSubject,
                RepeatEvery = dto.RepeatEvery,
                NoOfTimesToRepeat = dto.NoOfTimesToRepeat,
                RepeatNotification = dto.RepeatNotification,
                UseDocumentTemplate = dto.UseDocumentTemplate,
                DocumentTemplateID = dto.DocumentTemplateID,
                CreatedBy = "System",
                CreatedDate = DateTimeOffset.UtcNow
            };

            _context.NotificationMessages.Add(notification);
            await _context.SaveChangesAsync();
           

            return CreatedAtAction(nameof(GetById), new { id = notification.NotificationMessageID }, notification);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateNotificationDto dto)
        {
            if (id != dto.NotificationMessageID)
                return BadRequest("ID mismatch");

            var entity = await _context.NotificationMessages.FindAsync(id);
            if (entity == null) return NotFound();

            entity.NotificationChannel = dto.NotificationChannel;
            entity.NotificationHeading = dto.NotificationHeading;
            entity.NotificationBody = dto.NotificationBody;
            entity.NotificationFooter = dto.NotificationFooter;
            entity.NotificationSubject = dto.NotificationSubject;
            entity.RepeatEvery = dto.RepeatEvery;
            entity.NoOfTimesToRepeat = dto.NoOfTimesToRepeat;
            entity.RepeatNotification = dto.RepeatNotification;
            entity.UseDocumentTemplate = dto.UseDocumentTemplate;
            entity.DocumentTemplateID = dto.DocumentTemplateID;
            entity.UpdatedBy = "system"; 
            entity.UpdatedDate = DateTimeOffset.UtcNow;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.NotificationMessages.FindAsync(id);
            if (item == null) return NotFound();

            _context.NotificationMessages.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
