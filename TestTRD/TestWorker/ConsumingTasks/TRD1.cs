using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace TestWorker.ConsumingTasks
{
    public class TRD1 : IConsumingTask<Ignore, string>
    {
        private readonly IKafkaProducerManager _producerManager;

        public TRD1(IKafkaProducerManager producerManager)
        {
            _producerManager = producerManager;
        }
        public void Execute(ConsumeResult<Ignore, string> result)
        {
            var resultDesizlizer = JsonSerializer.Deserialize<ClassRequest>(result.Message.Value);
            if(resultDesizlizer == null)
            {
                Console.WriteLine("Value message API is null");
            }
            else
            {
                resultDesizlizer.S2 = DateTime.Now.ToString("HH:mm:ss:fff");
            }

            var producer = _producerManager.GetProducer<Null, string>("topic1");
            var messageTRD = new Message<Null, string> { Value = JsonSerializer.Serialize(resultDesizlizer) };
            producer.Produce(messageTRD);
        }
    }
}
