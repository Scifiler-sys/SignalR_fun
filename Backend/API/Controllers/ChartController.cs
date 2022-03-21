using Backend.Hubs;
using BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChartController : ControllerBase
{
    private readonly IHubContext<ChartHub> _hub;
    private readonly IRepository<Population> _repo;
    private static int _count = 0;
    private static List<double> _maleDataset = new List<double>();

    public ChartController(IHubContext<ChartHub> hub, IRepository<Population> repo)
    {
        _hub = hub;
        _repo = repo;
    }

    /// <summary>
    /// Sends information to all clients that is currently listening to "transferPopulationData"
    /// Will only send data once and rejects any other requests for the data
    /// </summary>
    public IActionResult Get()
    {
        if (!RealTimeManager.alreadySending)
        {
            var timerManager = new RealTimeManager(() => {
                _hub.Clients.All.SendAsync("transferPopulationData",_repo.read()[_count < _repo.read().Count-1 ? _count++ : _count=0]);
            });

            return Ok(new { Message = "Request Completed" });
        }
        else
        {
            return Conflict(new {Message = "Already sending data!"});
        }
    }
}
