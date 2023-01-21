using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenielDavid.webdev.Infrastructure.Domain.Models
{
    public class Video
    {
        public Guid? VideoId { get; set; }
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public string? Title { get; set; }
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfPublish { get; set; }
        public Type? Type { get; set; }
        public Guid? ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }

    }
    public enum Type
    {
        Series = 1,
        Movie = 2
    }
}
