using System.Text.Json.Serialization;

namespace SB.Domain.Entities.Common
{
    public interface IEntity
    {
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
    }
}
