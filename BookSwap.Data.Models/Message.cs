using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSwap.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SenderId { get; set; }= null!;
        public ApplicationUser Sender { get; set; }=null!;
        [Required]
        public string ReceiverId { get; set; }= null!;
        public ApplicationUser Receiver { get; set; }=   null!;
        [Required]
        [MaxLength(600)]
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
