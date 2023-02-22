using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using System.Text.Json;

namespace TestWorker.ConsumingTasks
{
    public class Pusher : IConsumingTask<Ignore, string>
    {
        public void Execute(ConsumeResult<Ignore, string> result)
        {
            var resultDesizlizer = JsonSerializer.Deserialize<ClassRequest>(result.Message.Value);
            if (resultDesizlizer == null)
            {
                Console.WriteLine("Value message Acc is null");
            }
            else
            {
                resultDesizlizer.S5 = DateTime.Now.ToString("HH:mm:ss:fff");
            }
            
        }
    }
}
