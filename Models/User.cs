using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyExam.Models
{
    public class User
    {
       [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "First Name must be 2 characters or more")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be 2 characters or more")]
        public string LastName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Username must be between 3 and 15 character")]
        [MaxLength(15)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Soory, Password must be 8 characters or more")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }
        public List<Hobby> PostedHobbies { get; set; }
        public List<Like> LikedHobbies { get; set; }
    }
}