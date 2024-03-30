using ConferenceService.DBContext.Models;

namespace ConferenceService.DTO_models
{
    public class ApplicationDTO
    {
        public Guid? author {  get; set; }
        public string? activity { get; set; }
        public string? name {  get; set; }
        public string? description { get; set; }
        public string? outline { get; set; }
    }
}
