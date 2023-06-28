namespace NihilitterApi.Models
{
    public class Friendship
    {
        public long? Id { get; set; }
        public long? UserId{get;set;}
        public long? FriendId { get; set; }
        public Boolean isConfirmed { get; set; }

    }      

}