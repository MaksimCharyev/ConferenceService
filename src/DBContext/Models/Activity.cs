using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceService.DBContext.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public EnumTypeActivity activity {  get; set; }
        [Required]
        public string description { get; set; }
        [NotMapped]
        public string activityName
        {
            get { return activity.ToString(); }
        }
    }
}
