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
        [Column("user_id")]
        public int UserId { get; set; } = default(int);

        [Required]
        [Column("calculation_id")]
        public int CalculationId { get; set; } = default(int);
    }
}