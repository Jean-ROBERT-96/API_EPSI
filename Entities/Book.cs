using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("book")]
    public class Book : IEntity
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("title"), Required]
        public string Title { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("author_id"), Required]
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author? Author { get; set; }

        [Column("publisher_id"), Required]
        public int PublisherId { get; set; }
        [ForeignKey(nameof(PublisherId))]
        public Publisher? Publisher { get; set; }

        [Column("release")]
        public DateTime? Release { get; set; }
    }
}
