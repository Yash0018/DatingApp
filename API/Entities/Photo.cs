using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]   // Use this to name the table
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        // This will fully define our relationship with the AppUser Table
        public int AppUserId { get; set; }  // This will make sure that there are no photos with no AppUserId  
        public AppUser AppUser { get; set; }
    }
}