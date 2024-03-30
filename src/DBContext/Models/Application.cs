using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceService.DBContext.Models
{
    public class Application
    {
        string? _name;
        string? _description;
        string? _outline;
        [Key]
        public Guid id { get; set; }
        public Guid author { get; set; }
        public Activity? activity { get; set; }
        public DateTime createdAt { get; set; }
        public string? Name 
        {
            get { return _name; }
            set { if (value != null && value.Length > 100) { throw new ArgumentOutOfRangeException(
                "Name",value,"переменная содержит больше чем 100 символов"); } 
                  else { _name = value; } }
        }
        public string? Description 
        {
            get { return _description; } 
            set { if(value != null && value.Length > 300) { throw new ArgumentOutOfRangeException(
                "Description",value,"переменная содержит больше чем 300 символов"); } 
                  else {  _description = value; } }
        }
        public string? Outline 
        { 
            get { return _outline; } 
            set { if(value != null && value.Length > 1000) {  throw new ArgumentOutOfRangeException(
                "Outline",value,"переменная содержит больше чем 1000 символов"); } 
                  else {  _outline = value; } }
        }
        public Application()
        {
            
        }
        public Application(Guid AuthorID, Activity? type, string? name, string? description, string? outline, DateTime createdAt)
        {
            id = new Guid();
            this.author = AuthorID;
            this.activity = type;
            Name = name;
            Description = description;
            Outline = outline;
            this.createdAt = createdAt;
        }
    }
}
