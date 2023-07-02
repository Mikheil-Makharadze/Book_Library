using System.ComponentModel.DataAnnotations;

namespace Clinet.Models.DTO
{
    public class BookCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        public bool IsTaken { get; set; } = false;

        //Relationship
        [Required]
        public virtual ICollection<int> AuthorsId { get; set; }
    }
}
