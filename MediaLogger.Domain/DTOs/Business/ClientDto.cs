using MediaLogger.Domain.Entities.Business;

namespace MediaLogger.Domain.DTOs
{
    public class ClientDto : DtoCommon
    {
        public string? Name { get; set; }
        public string? Nit { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int IdRegion { get; set; }
        public string? Region { get; set; }
        public List<byte> PreLogoImg { get; set; } = new List<byte>();
        public byte[]? LogoImg { get; set; }
        public string? ImgExt { get; set; }
        public List<OfficeDto> Offices { get; set; } = new List<OfficeDto>();
        public int IdUserLinked { get; set; }
        public string? UserLinked { get; set; }
        

    }
}
