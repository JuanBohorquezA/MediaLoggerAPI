namespace MediaLogger.Domain.Entities.Business
{
    public class User: EntityCommon
    {
        public string? PWD { get; set; }
        public string DOCUMENT { get; set; }
        public int ID_TYPE_DOCUMENT { get; set; }
        public string? TYPE_DOCUMENT { get; set; }
        public string USERNAME { get; set; }    
        public string? NAME { get; set; }
        public string? LASTNAME { get; set; }
        public string? EMAIL { get; set; }
        public string? PHONE { get; set; }
        public int ID_ROLE { get; set; }
        public string? ROLE { get; set; }
        public int STATUS { get; set; }
        public byte[]? PROFILE_IMG { get; set; }
    }
}