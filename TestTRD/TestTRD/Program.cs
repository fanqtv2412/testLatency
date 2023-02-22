using Confluent.Kafka;

class Program
{
    static void Main(string[] args)
    {
        var conf = new ConsumerConfig
        {
            GroupId = "my-group-id",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var configProducer = new ProducerConfig
        {
            BootstrapServers = "9092",
        };
        var produce = new ProducerBuilder<Null, string>(configProducer).Build();
        var messageTRD = new Message<Null, string> { Value = "{\"ClientCode\":\"058C108101\",\"Amount\":25000000}" };

        using (var consumer = new ConsumerBuilder<Ignore, string>(conf).Build())
        {
            consumer.Subscribe("BDRD-test-latency-api");

            try
            {
                while (true)
                {
                    var message = consumer.Consume();
                    Console.WriteLine($"Received message at {message.TopicPartitionOffset}: {message.Message.Value}");
                    produce.Produce("BDRD-test-latency-trd", messageTRD);
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}