
using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Extensions;
using Manonero.MessageBus.Kafka.Settings;
using TestWorker.ConsumingTasks;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddKafkaConsumers(consumerBuilder =>
        {
            consumerBuilder.AddConsumer<TRD1>(ConsumerSetting.MapValue(context.Configuration.GetSection("Consumer1")));
            consumerBuilder.AddConsumer<Acc>(ConsumerSetting.MapValue(context.Configuration.GetSection("Consumer2")));
            consumerBuilder.AddConsumer<TRD2>(ConsumerSetting.MapValue(context.Configuration.GetSection("Consumer3")));
            consumerBuilder.AddConsumer<Pusher>(ConsumerSetting.MapValue(context.Configuration.GetSection("Consumer4")));

        });
        services.AddKafkaProducers(producerBuilder =>
        {
            producerBuilder.AddProducer(ProducerSetting.MapValue(context.Configuration.GetSection("Producer1")));
            producerBuilder.AddProducer(ProducerSetting.MapValue(context.Configuration.GetSection("Producer2")));
            producerBuilder.AddProducer(ProducerSetting.MapValue(context.Configuration.GetSection("Producer3")));
        });
    })
    .Build();
host.UseKafkaMessageBus(messageBus =>
{
    messageBus.RunConsumer<Ignore, string>("topic1");
    messageBus.RunConsumer<Ignore, string>("topic2");
    messageBus.RunConsumer<Ignore, string>("topic3");
    messageBus.RunConsumer<Ignore, string>("topic4");
});
await host.RunAsync();
