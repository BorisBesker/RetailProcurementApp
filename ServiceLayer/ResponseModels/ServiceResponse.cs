namespace ServiceLayer.Models
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public bool RecordExists { get; set; }
        public bool UserNameExists { get; set; }
        public bool ExistSameName { get; set; }
        public bool RelationShipEntityMissing { get; set; }
        public T? Entity { get; set; }
    }
}
