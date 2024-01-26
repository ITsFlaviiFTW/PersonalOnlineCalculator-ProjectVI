using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalOnlineCalculator.Models
{
    [Table("calculation")]
    public class Calculation
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; } = default(int);

        [Required]
        [Column("expression")]
        public string Expression { get; set; } = null!;

        [Required]
        [Column("result")]
        public string Result { get; set; } = null!;
    }
}