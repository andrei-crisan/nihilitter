namespace NihilitterApi.Dto
{
    public class NihilDto
    {
        public long? Id { get; set; }
        public string? Post { get; set; }
        public DateTime PostDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public long? UserId { get; set; }
    }
}