using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalOnlineCalculator.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; } = default(int);

        [Required]
        [Column("name")]
        public string Username { get; set; } = null!;

        [Required]
        [Column("name")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("name")]
        public string PasswordHash { get; set; } = null!;

    }
}