namespace Tabkhity.Core.Entities
{
    public class Lunch : AuditEntity<int>
    {
        public string Name { get; set; }
        public string UserId { get; set; }
    }
}
