using System.ComponentModel.DataAnnotations;

namespace ConferenceService.DBContext.Models
{
    public class SubmittedApplication
    {
        [Key]
        public int id { get; set; }
        [Required]
        public Application application { get; set; }
        [Required]
        public DateTime sumbittedAt { get; set; }  
        public SubmittedApplication() 
        {
            sumbittedAt = DateTime.Now;
        }
    }
}
