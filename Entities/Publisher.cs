using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("publisher")]
    public class Publisher : IEntity
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name"), Required]
        public string Name { get; set; }

        [Column("founding")]
        public DateTime? Founding { get; set; }
    }
}
