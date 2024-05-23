using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("author")]
    public class Author : IEntity
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("firstname"), Required]
        public string Firstname { get; set; }

        [Column("lastname"), Required]
        public string Lastname { get; set; }

        [Column("born")]
        public DateTime? Born { get; set; }
    }
}
