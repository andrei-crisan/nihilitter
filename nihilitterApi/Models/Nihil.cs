using System.Text.Json.Serialization;

namespace NihilitterApi.Models
{
    public class Nihil
    {
        public long Id { get; set; }
        public String? Post { get; set; }
        public DateTime PostDate { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public long? UserId { get; set; }
    }
}