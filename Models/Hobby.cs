using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HobbyExam.Models
{
    public class Hobby
    {
        [Key]
        [Required]
        public int HobbyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User PostedBy { get; set; }
        public List<Like> Fans { get; set; }
    }
}