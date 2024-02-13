

namespace MediaLogger.Domain.DTOs
{
    public class UserDto: DtoCommon
    {
        public string? Pwd { get; set; }
        public string? Document { get; set; }
        public int IdTypeDocument { get; set; }
        public string? TypeDocument { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int IdRole { get; set; }
        public string? Role { get; set; }
        public int Status { get; set; }
        public byte[]? ProfileImg { get; set; }
    }
}
