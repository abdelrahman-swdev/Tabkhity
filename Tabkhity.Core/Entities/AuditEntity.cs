namespace Tabkhity.Core.Entities
{
    public class AuditEntity<T> : BaseEntity<T>
    {
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
