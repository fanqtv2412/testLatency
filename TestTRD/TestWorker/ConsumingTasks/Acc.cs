using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using System.Text.Json;

namespace TestWorker.ConsumingTasks
{
    public class Acc : IConsumingTask<Ignore, string>
    {
        private readonly IKafkaProducerManager _producerManager;
        public Acc(IKafkaProducerManager producerManager)
        {
            _producerManager = producerManager;
        }
        public void Execute(ConsumeResult<Ignore, string> result)
        {
            var resultDesizlizer = JsonSerializer.Deserialize<ClassRequest>(result.Message.Value);
            if (resultDesizlizer == null)
            {
                Console.WriteLine("Value message TRD1 is null");
            }
            else
            {
                resultDesizlizer.S3 = DateTime.Now.ToString("HH:mm:ss:fff");
            }

            var produce = _producerManager.GetProducer<Null, string>("topic2");
            var messageAcc = new Message<Null, string> { Value = JsonSerializer.Serialize(resultDesizlizer) };
            produce.Produce(messageAcc);
        }
    }
}
