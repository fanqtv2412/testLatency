using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using System.Text.Json;

namespace TestWorker.ConsumingTasks
{
    public class TRD2 : IConsumingTask<Ignore, string>
    {
        private readonly IKafkaProducerManager _producerManager;
        public TRD2(IKafkaProducerManager producerManager)
        {
            _producerManager = producerManager;
        }
        public void Execute(ConsumeResult<Ignore, string> result)
        {
            var resultDesizlizer = JsonSerializer.Deserialize<ClassRequest>(result.Message.Value);
            if (resultDesizlizer == null)
            {
                Console.WriteLine("Value message Acc is null");
            }
            else
            {
                resultDesizlizer.S4 = DateTime.Now.ToString("HH:mm:ss:fff");
            }
            var produce = _producerManager.GetProducer<Null, string>("topic3");
            var messageTRD = new Message<Null, string> { Value = JsonSerializer.Serialize(resultDesizlizer) };
            produce.Produce(messageTRD);
        }
    }
}
