
using Confluent.Kafka;
using System.Diagnostics;
using System.Net;
using static Confluent.Kafka.ConfigPropertyNames;

namespace PolicyAPI.Repositories
{
    public class PolicyPublishRepo : IPolicyPublishRepo
    {
        public async Task<string> PublishPolicyData(string TopicName, string Message, 
            IConfiguration configuration)
        {
            ProducerConfig ProducerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["BootstrapServer"],
                ClientId = Dns.GetHostName()
            };
            try
            {
                using (var producerBuilder = new ProducerBuilder<string, string>
                    (ProducerConfig).Build())
                {
                    var result = await producerBuilder.ProduceAsync
                   (TopicName, new Message<string, string>
                   {
                       Key = new Random().Next(5).ToString(),
                       Value = Message
                   });

                    Debug.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
                    producerBuilder.Flush(TimeSpan.FromSeconds(60));
                    return await Task.FromResult($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
            return await Task.FromResult("Not Published.....");

        }
    }
}
