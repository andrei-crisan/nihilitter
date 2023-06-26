namespace NihilitterApi.Dto
{
    public class FriendDto
    {
        public long? Id { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Country { get; set; }
        public String? Email { get; set; }
        public Boolean? IsConfirmed { get; set;}
    }
}