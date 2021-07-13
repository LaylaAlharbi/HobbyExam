using System.ComponentModel.DataAnnotations;

namespace HobbyExam.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public User Fan { get; set; }
        public int HobbyId { get; set; }
        public Hobby FanOf { get; set; }
    }
}