using NihilitterApi.Models;

namespace NihilitterApi.Dto
{
    public class UserDto
    {
        public long? Id { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Country { get; set; }
        public String? Email { get; set; }
        public List<Friendship>? Friends { get; set; }
    }
}