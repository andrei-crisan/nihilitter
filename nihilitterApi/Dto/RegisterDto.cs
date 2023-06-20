namespace NihilitterApi.Dto
{
    public class RegisterDto
    {
        public long? Id { get; set; }
        public String? Email { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Country { get; set; }
        public String? Password { get; set; }
    }
}