using System.Text.Json.Serialization;

namespace NihilitterApi.Models
{
    public class Message
    {
        public long? Id { get; set; }
        [JsonIgnore]
        public User? From { get; set; }
        public long? FromId { get; set; }
        public String? MessageBody { get; set; }
        [JsonIgnore]
        public User? To { get; set; }
        public long? ToId { get; set; }

    }
}