using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TestLatency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetReponse(int amount = 1)
        {
            var requestMSG = new ClassRequest();
            requestMSG.S0 = DateTime.Now.ToString("HH:mm:ss:fff");
            requestMSG.S1 = DateTime.Now.ToString("HH:mm:ss:fff");
            var msg = JsonSerializer.Serialize(requestMSG);
            var message = new Message<Null, string> { Value = msg};
            var configProducer = new ProducerConfig
            {
                BootstrapServers = "10.26.7.58",
            };
            Console.WriteLine("Start: " + DateTime.Now.ToString("HH:mm:ss:fff"));
            var produce = new ProducerBuilder<Null, string>(configProducer).Build();
            for(int i =0; i< amount; i++)
            {
                produce.Produce("BDRD-test-latency-api", message);

            }
            return Ok();
        }
    }
}
