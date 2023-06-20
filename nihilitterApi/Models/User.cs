namespace NihilitterApi.Models
{
    public class User
    {
        public long? Id { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Country { get; set; }
        public String? Email { get; set; }
        public String? Password { get; set; }
        public List<User>? Friends { get; set; }
        public List<Message>? Messages{get;set;}
    }
}