using MassTransit;
using Sample.Rabbit.Core.Events;

namespace Sample.Rabbit.Api.Controllers;

[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IPublishEndpoint _publisher;
    private readonly ILogger<UserController> _logger;
    private readonly IMessageScheduler _publisherScheduler;

    public UserController(IPublishEndpoint publisher, ILogger<UserController> logger, IMessageScheduler publisherScheduler)
    {
        _publisher = publisher;
        _logger = logger;
        _publisherScheduler = publisherScheduler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InsertUserEvent insertEvent)
    {
        await _publisher.Publish(insertEvent);
        var textMessage = $"Send user inserted: {insertEvent.Id} - {insertEvent.Name}";
        _logger.LogInformation(textMessage);

        return Ok(new { message = textMessage });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserEvent updateEvent)
    {
        await _publisher.Publish(updateEvent);
        var textMessage = $"Send user updated: {updateEvent.Id} - {updateEvent.Name}";

        return Ok(new { message = textMessage });
    }

    [HttpPost("schedule")]
    public async Task<IActionResult> Schedule([FromBody] InsertUserEvent scheduleEvent)
    {
        await _publisherScheduler.SchedulePublish(DateTime.Now + TimeSpan.FromSeconds(10), scheduleEvent);
        var textMessage = $"Send user updated: {scheduleEvent.Id} - {scheduleEvent.Name}";

        _logger.LogInformation(textMessage);

        return Ok(new { message = textMessage });
    }
}

