using BlazorViewer.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic;

namespace BlazorViewer.Controllers
{
    public class SinkModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SinkController : ControllerBase
    {
        private readonly IHubContext<TimeHub> _context;

        public SinkController(IHubContext<TimeHub> context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<SinkModel>> PostAsync(SinkModel model)
        {
            // Add your logic here to handle the POST request
            await _context.Clients.All.SendAsync("RefreshMessage", model.Message);
            //return CreatedAtAction("GetMyModel", new { id = model.Id }, model);
            return Ok();
        }
    }
}
