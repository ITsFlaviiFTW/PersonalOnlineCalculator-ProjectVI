using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalOnlineCalculator.Models
{
    [Table("user_calculations")]
    public class User_Calculations
    {
        [Key]
        [Required]
        [Column("userid")]
        public int UserId { get; set; } = default(int);

        [Key]
        [Required]
        [Column("calculationid")]
        public int CalculationId { get; set; } = default(int);
    }
}