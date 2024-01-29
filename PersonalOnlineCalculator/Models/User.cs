using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalOnlineCalculator.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; } = default(int);

        [Required]
        [Column("username")]
        public string Username { get; set; } = null!;

        [Required]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("password")]
        public string PasswordHash { get; set; } = null!;

    }
}