using System.Text.Json.Serialization;

namespace SB.Domain.Entities.Common
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {

        }
        [JsonPropertyOrder(-1000)]
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
#nullable enable
        public string? UpdatedBy { get; set; }
#nullable disable
        public DateTime? UpdatedDate { get; set; }
        public int? StatusId { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasId { get => Id > 0; }
    }
}
