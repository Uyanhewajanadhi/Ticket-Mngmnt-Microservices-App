using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using System;
using System.Threading.Tasks;

namespace TicketingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketingsPublisherController : ControllerBase
    {
        private readonly IBus _bus;
        public TicketingsPublisherController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ticket != null)
            {
                ticket.BookedOn = DateTime.Now;
                
                Uri uri = new Uri("rabbitmq://localhost/theTicketQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(ticket);
                return Ok;
            }
            return BadRequest;
        }

        private new IActionResult BadRequest => throw new NotImplementedException();

        private new IActionResult Ok => throw new NotImplementedException();
    }
}
