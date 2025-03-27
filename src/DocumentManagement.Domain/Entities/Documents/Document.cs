
namespace DocumentManagement.Domain.Entities;
    public class Document
    {
        public Guid Key {get; set;}
        public string FileName {get; set;} = String.Empty;
        public string FileUrl {get; set;} = String.Empty;
    }

