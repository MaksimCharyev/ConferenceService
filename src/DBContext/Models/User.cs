using ConferenceService.DBContext.Models;
using System.ComponentModel.DataAnnotations;
namespace ConferenceService.DBContext.Models
{
    public class User
    {
        [Key]
        public Guid id { get; set; }
        public Application? currentApplication { get; set; }
        public User(Guid id) 
        {
            this.id = id;
        }
    }
}
